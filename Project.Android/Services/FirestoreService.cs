using System;
using Firebase.Firestore;

namespace FirestoreRe.Droid.Services
{
    public static class FirestoreService
    {
        public static string AppName { get; } = "SampleAppName";
        public static Firebase.FirebaseApp app;
        
        public static FirebaseFirestore Instance
        {
            get
            {
                return FirebaseFirestore.GetInstance(app);
            }
        }
        public static void Init(Android.Content.Context context)
        {
            try
            {
                var baseOptions = Firebase.FirebaseOptions.FromResource(context);
                var options = new Firebase.FirebaseOptions.Builder(baseOptions).SetProjectId("flightsappxamarin").Build();
                app = Firebase.FirebaseApp.InitializeApp(context, options, AppName);
            }
            catch (Exception)
            { 
                throw;
            }

        }
    }
}
