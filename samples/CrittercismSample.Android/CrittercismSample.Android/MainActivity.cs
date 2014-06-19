﻿using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using System.Threading.Tasks;
using System.Threading;
using Org.Json;

using CrittercismAndroid;

namespace CrittercismSample.Android
{
	[Activity (Label = "CrittercismSample.Android", MainLauncher = true)]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			//Initialize Crittercism
			Crittercism.Init( ApplicationContext,  "53a20750178784113a000001");

			//Set the Username
			Crittercism.SetUserName ("ANDROID_USER_NAME");

			Crittercism.OptOutStatus = true;

			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			Button buttonAttachMetadata = FindViewById<Button> (Resource.Id.buttonAttachMeta);
			buttonAttachMetadata.Click += delegate {
				Crittercism.SetMetadata( "MyKey", "MyValue" );
				buttonAttachMetadata.Text = string.Format ("Metadata sent!");
			};

			/*
			Button buttonCrashNative = FindViewById<Button> (Resource.Id.buttonCrashNative);
			buttonCrashNative.Click += delegate {
				Crittercism.LeaveBreadcrumb( "Crash Native");
				crashNative();
			};
			
			Button buttonNativeException = FindViewById<Button> (Resource.Id.buttonNativeException);
			buttonNativeException.Click += delegate(object sender, EventArgs e) {
				Crittercism.LeaveBreadcrumb( "Native Exception");
				nativeException();
			};
			*/

			//TODO: Implement LogHandledException
//			Button buttonHandledException = FindViewById<Button> (Resource.Id.buttonHandledException);
//			buttonHandledException.Click += delegate(object sender, EventArgs e) {
//				try {
////					crashDivideByZero();
//				} catch (Exception error){
//				}
//			};

			Button buttonCrashCLR = FindViewById<Button> (Resource.Id.buttonCrashCLR);
			buttonCrashCLR.Click += delegate(object sender, EventArgs e) {
				Crittercism.LeaveBreadcrumb( "Crash CLR");
				crashCLR();
			};

			Button buttonLeaveBreadcrumb = FindViewById<Button> (Resource.Id.buttonBreadcrumb);
			buttonLeaveBreadcrumb.Click += delegate(object sender, EventArgs e) {
				Crittercism.LeaveBreadcrumb( "Android BreadCrumb");
				buttonLeaveBreadcrumb.Text = string.Format( "just left a breadcrumb");
			};
		}

		public void crashCLR()
		{
			crashNullReference ();
		}

		private void crashNullReference()
		{
			object o = null;
			o.GetHashCode ();
		}

		public void crashDivideByZero()
		{
			int i = 0;
			i = 2 / i;
		}

		public void crashIndexOutOfRange()
		{
			string[] arr	= new string[1];
			arr[2]	= "Crash";
		}

		// +++++++++++++++++++++++++++++++++++++++
		// Additional Crash & Exception
		// +++++++++++++++++++++++++++++++++++++++

		public void nativeException()
		{
			throw new Java.Lang.IllegalArgumentException();
		}

		public void crashBackgroundThread()
		{
			ThreadPool.QueueUserWorkItem(o => { 
				throw new Exception("Crashed Background thread."); 
			} );
		}

		public async Task CrashAsync()
		{
			await Task.Delay(10).ConfigureAwait(false);
			throw new InvalidOperationException("Exception in task");
		}
	}
}


