using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using Newtonsoft.Json;
using Microsoft.JScript;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace FileOCX
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    [ProgId("VTM.FileActiveX")]
    [ClassInterface(ClassInterfaceType.AutoDual)] //Implementing interface that will be visible from JS
    [Guid("EFA92E6A-1DFB-4F6C-A51E-5E094213510A")]
    [ComVisible(true)]
    public class ActiveXObject : IObjectSafety
    {
        private string _directory = @"";

        private const string _IID_IDispatch = "{00020400-0000-0000-C000-000000000046}";
        private const string _IID_IDispatchEx = "{a6ef9860-c720-11d0-9337-00a0c90dcaa9}";
        private const string _IID_IPersistStorage = "{0000010A-0000-0000-C000-000000000046}";
        private const string _IID_IPersistStream = "{00000109-0000-0000-C000-000000000046}";
        private const string _IID_IPersistPropertyBag = "{37D84F60-42CB-11CE-8135-00AA004BB851}";

        private const int INTERFACESAFE_FOR_UNTRUSTED_CALLER = 0x00000001;
        private const int INTERFACESAFE_FOR_UNTRUSTED_DATA = 0x00000002;
        private const int S_OK = 0;
        private const int E_FAIL = unchecked((int)0x80004005);
        private const int E_NOINTERFACE = unchecked((int)0x80004002);

        private bool _fSafeForScripting = true;
        private bool _fSafeForInitializing = true;

        //public ActiveXObject()
        //{
        //    _directory = 
        //}

        /// <summary>
        /// Opens application. Called from JS
        /// </summary>
        [ComVisible(true)]
        public string GetFilesJson(int count)
        {
            try
            {
                var arr = System.IO.Directory.GetFiles(_directory);

                return JsonConvert.SerializeObject(arr);
            }
            catch (Exception e)
            {
                //ExceptionHandling.AppException(e);
                throw e;
            }
        }


        [ComVisible(true)]
        public ArrayObject GetFiles(int count)
        {
            try
            {
                var di = new DirectoryInfo(_directory);
                var files = di.GetFiles();

                FileInfo temp;
                for (int i = 0; i < files.Length; i++)
                {
                    for (int j = i + 1; j < files.Length; j++)
                    {
                        if (files[j].CreationTime > files[i].CreationTime)
                        {
                            temp = files[j];
                            files[j] = files[i];
                            files[i] = temp;
                        }
                    }
                }

                var fileNames = new string[files.Length];
                for (int i = 0; i < files.Length; i++)
                {
                    fileNames[i] = files[i].FullName;
                }

                if (count < fileNames.Length)
                {
                    var newArr = new string[count];
                    for (int i = 0; i < count; i++)
                    {
                        newArr[i] = fileNames[i];
                    }
                    return GlobalObject.Array.ConstructArray(newArr);
                }
                else
                {
                    return GlobalObject.Array.ConstructArray(fileNames);
                }
                
            }
            catch (Exception e)
            {
                //ExceptionHandling.AppException(e);
                throw e;
            }
        }


        [ComVisible(true)]
        public string GetImage(string file)
        {
            try
            {
                //var files = System.IO.Directory.GetFiles(_directory);
                if (File.Exists(file))
                {
                    return ToBase64(file);
                }
                return "";
            }
            catch (Exception e)
            {
                //ExceptionHandling.AppException(e);
                throw e;
            }
        }

        /// <summary>
        /// Parameter visible from JS
        /// </summary>
        [ComVisible(true)]
        public string Directory
        {
            get
            {
                return _directory;
            }
            set
            {
                _directory = value;
            }
        }

        ///	<summary>
        ///	Register the class as a	control	and	set	it's CodeBase entry
        ///	</summary>
        ///	<param name="key">The registry key of the control</param>
        [ComRegisterFunction()]
        public static void RegisterClass(string key)
        {
            // Strip off HKEY_CLASSES_ROOT\ from the passed key as I don't need it
            StringBuilder sb = new StringBuilder(key);

            sb.Replace(@"HKEY_CLASSES_ROOT\", "");
            // Open the CLSID\{guid} key for write access
            RegistryKey k = Registry.ClassesRoot.OpenSubKey(sb.ToString(), true);

            // And create	the	'Control' key -	this allows	it to show up in
            // the ActiveX control container
            RegistryKey ctrl = k.CreateSubKey("Control");
            ctrl.Close();

            // Next create the CodeBase entry	- needed if	not	string named and GACced.
            RegistryKey inprocServer32 = k.OpenSubKey("InprocServer32", true);
            inprocServer32.SetValue("CodeBase", Assembly.GetExecutingAssembly().CodeBase);
            inprocServer32.Close();
            // Finally close the main	key
            k.Close();
            MessageBox.Show("Registered");
        }

        ///	<summary>
        ///	Called to unregister the control
        ///	</summary>
        ///	<param name="key">Tke registry key</param>
        [ComUnregisterFunction()]
        public static void UnregisterClass(string key)
        {
            StringBuilder sb = new StringBuilder(key);
            sb.Replace(@"HKEY_CLASSES_ROOT\", "");

            // Open	HKCR\CLSID\{guid} for write	access
            RegistryKey k = Registry.ClassesRoot.OpenSubKey(sb.ToString(), true);

            // Delete the 'Control'	key, but don't throw an	exception if it	does not exist
            k.DeleteSubKey("Control", false);

            // Next	open up	InprocServer32
            //RegistryKey	inprocServer32 = 
            k.OpenSubKey("InprocServer32", true);

            // And delete the CodeBase key,	again not throwing if missing
            k.DeleteSubKey("CodeBase", false);

            // Finally close the main key
            k.Close();
            MessageBox.Show("UnRegistered");
        }

        private string ToBase64(string file)
        {
            using (Image image = Image.FromFile(file))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    var header = GetHeader(image.RawFormat.Guid);
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = System.Convert.ToBase64String(imageBytes);
                    return header+base64String;
                }
            }
        }

        public int GetInterfaceSafetyOptions(ref Guid riid, ref int pdwSupportedOptions, ref int pdwEnabledOptions)
        {
            int Rslt = E_FAIL;

            string strGUID = riid.ToString("B");
            pdwSupportedOptions = INTERFACESAFE_FOR_UNTRUSTED_CALLER | INTERFACESAFE_FOR_UNTRUSTED_DATA;
            switch (strGUID)
            {
                case _IID_IDispatch:
                case _IID_IDispatchEx:
                    Rslt = S_OK;
                    pdwEnabledOptions = 0;
                    if (_fSafeForScripting == true)
                        pdwEnabledOptions = INTERFACESAFE_FOR_UNTRUSTED_CALLER;
                    break;
                case _IID_IPersistStorage:
                case _IID_IPersistStream:
                case _IID_IPersistPropertyBag:
                    Rslt = S_OK;
                    pdwEnabledOptions = 0;
                    if (_fSafeForInitializing == true)
                        pdwEnabledOptions = INTERFACESAFE_FOR_UNTRUSTED_DATA;
                    break;
                default:
                    Rslt = E_NOINTERFACE;
                    break;
            }

            return Rslt;
        }

        public int SetInterfaceSafetyOptions(ref Guid riid, int dwOptionSetMask, int dwEnabledOptions)
        {
            int Rslt = E_FAIL;

            string strGUID = riid.ToString("B");
            switch (strGUID)
            {
                case _IID_IDispatch:
                case _IID_IDispatchEx:
                    if (((dwEnabledOptions & dwOptionSetMask) == INTERFACESAFE_FOR_UNTRUSTED_CALLER) &&
                            (_fSafeForScripting == true))
                        Rslt = S_OK;
                    break;
                case _IID_IPersistStorage:
                case _IID_IPersistStream:
                case _IID_IPersistPropertyBag:
                    if (((dwEnabledOptions & dwOptionSetMask) == INTERFACESAFE_FOR_UNTRUSTED_DATA) &&
                            (_fSafeForInitializing == true))
                        Rslt = S_OK;
                    break;
                default:
                    Rslt = E_NOINTERFACE;
                    break;
            }

            return Rslt;
        }

        private string GetHeader(Guid id)
        {
            if (id == ImageFormat.Gif.Guid)
            {
                return "data:image/gif;base64,";
            }
            if (id == ImageFormat.Jpeg.Guid)
            {
                return "data:image/jpeg;base64,";
            }
            if (id == ImageFormat.Png.Guid)
            {
                return "data:image/png;base64,";
            }
            if (id == ImageFormat.Bmp.Guid)
            {
                return "data:image/bmp;base64,";
            }
            return "";
        }
    }


    [ComImport, GuidAttribute("CB5BDC81-93C1-11CF-8F20-00805F2CD064")]
     [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
     public interface IObjectSafety
     {
        
         [PreserveSig]
         int GetInterfaceSafetyOptions(ref Guid riid, [MarshalAs(UnmanagedType.U4)] ref int pdwSupportedOptions, [MarshalAs(UnmanagedType.U4)] ref int pdwEnabledOptions);
 
         [PreserveSig()]
         int SetInterfaceSafetyOptions(ref Guid riid, [MarshalAs(UnmanagedType.U4)] int dwOptionSetMask, [MarshalAs(UnmanagedType.U4)] int dwEnabledOptions);
     }
 
}