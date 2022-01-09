//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace InsAndOuts.Droid
//{
//    //BENDO: Finish updating this Activity to open DB3 files in this app os the DB can be restored.
//    //Code from: https://gist.github.com/brendanzagaeski/70afe08654927befb26b

//    [Activity(Label = "Db3FileActivity")]
//    //
//    // Example Intent for link in web browser:
//    // START u0 {act=android.intent.action.VIEW dat=http://www.example.com/test.foo typ=application/octet-stream}
//    //
//    // Example Intent for link in e-mail:
//    // START u0 {act=android.intent.action.VIEW dat=http://www.example.com/test.foo flg=0x90000}
//    //
//    [IntentFilter(new string[] { Intent.ActionView },
//                  Categories = new string[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
//                  DataScheme = "http",
//                  DataHost = "*",
//                  // Double-escape the backslashes in C# so they end up
//                  // single-escaped in the generated manifest file
//                  // `obj/Debug/android/AndroidManifest.xml`
//                  DataPathPattern = ".*\\\\.foo"
//                 )]

//    //
//    // Example Intent from tapping attachment in Email app
//    // START u0 {act=android.intent.action.VIEW dat=content://com.android.email.attachmentprovider/1/1/RAW typ=application/foo flg=0x80001}
//    //
//    [IntentFilter(new string[] { Intent.ActionView },
//                  Categories = new string[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
//                  DataScheme = "content",
//                  DataHost = "*",
//                  DataMimeType = "application/foo"
//                 )]
//    public class Db3FileActivity : Activity
//    {
//        protected override void OnCreate(Bundle savedInstanceState)
//        {
//            base.OnCreate(savedInstanceState);

//            // Create your application here
//        }
//    }
    
//    [Activity(Label = "DownloadsGmailAppsFileActivity")]
//    //
//    // Example intent from tapping file in Downloads app
//    // START u0 {act=android.intent.action.VIEW dat=content://downloads/all_downloads/1 typ=application/octet-stream flg=0x3}
//    //
//    // Example Intent from tapping attachment in Gmail app
//    // START {act=android.intent.action.VIEW dat=content://gmail-ls/user@gmail.com/messages/4/attachments/0.1/BEST/false typ=application/octet-stream flg=0x80001 u=0}
//    //
//    // This IntentFilter isn't so useful because it will match _any_ file
//    // that has the "application/octet-stream" MIME type, not just `foo`
//    // files.
//    // 
//    [IntentFilter(new string[] { Intent.ActionView },
//                  Categories = new string[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
//                  DataScheme = "content",
//                  DataHost = "*",
//                  DataMimeType = "application/octet-stream"
//                 )]
//    public class DownloadsGmailAppsFileActivity : Activity
//    {
//    }
//}