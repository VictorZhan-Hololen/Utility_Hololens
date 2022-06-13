using Microsoft.MixedReality.QR;
using System;
using UnityEngine;
using Victor.Utility;

namespace Victor.Hololens.Managers
{
    public class QRCodeManager : Singleton<QRCodeManager>
    {
        [Tooltip("Determines if the QR codes scanner should be automatically started.")]
        public bool AutoStartQRTracking = true;

        public bool IsTrackerRunning { get; private set; }

        public bool IsSupported { get; private set; }

        public event EventHandler<QRCode> OnQRCodeAdded;
        public event EventHandler<QRCode> OnQRCodeUpdated;
        public event EventHandler<QRCode> OnQRCodeRemoved;

        private QRCodeWatcher qrTracker;
        private bool capabilityInitialized = false;
        private QRCodeWatcherAccessStatus accessStatus;
        private System.Threading.Tasks.Task<QRCodeWatcherAccessStatus> capabilityTask;

        async protected virtual void Start()
        {
            IsSupported = QRCodeWatcher.IsSupported();
            capabilityTask = QRCodeWatcher.RequestAccessAsync();
            accessStatus = await capabilityTask;
            capabilityInitialized = true;
        }

        private void Update()
        {
            if (qrTracker == null && capabilityInitialized && IsSupported)
            {
                if (accessStatus == QRCodeWatcherAccessStatus.Allowed) SetupQRTracking();
                else Debug.Log("Capability access status : " + accessStatus);
            }
        }

        private void SetupQRTracking()
        {
            try
            {
                qrTracker = new QRCodeWatcher();
                IsTrackerRunning = false;
                qrTracker.Added += QRCodeWatcher_Added;
                qrTracker.Updated += QRCodeWatcher_Updated;
                qrTracker.Removed += QRCodeWatcher_Removed;
                qrTracker.EnumerationCompleted += QRCodeWatcher_EnumerationCompleted;
            }
            catch (Exception ex)
            {
                Debug.Log("QRCodesManager : exception starting the tracker " + ex.ToString());
            }

            if (AutoStartQRTracking) StartQRTracking();
        }

        public void StartQRTracking()
        {
            if (qrTracker != null && !IsTrackerRunning)
            {
                Debug.Log("QRCodesManager starting QRCodeWatcher");
                try
                {
                    qrTracker.Start();
                    IsTrackerRunning = true;
                }
                catch (Exception ex)
                {
                    Debug.Log("QRCodesManager starting QRCodeWatcher Exception:" + ex.ToString());
                }
            }
        }

        private void OnDestroy() => StopQRTracking();

        public void StopQRTracking()
        {
            if (IsTrackerRunning)
            {
                IsTrackerRunning = false;
                if (qrTracker != null)
                {
                    qrTracker.Stop();
                }
            }
        }

        private void QRCodeWatcher_Added(object sender, QRCodeAddedEventArgs args) => OnQRCodeAdded?.Invoke(sender, args.Code);
        private void QRCodeWatcher_Updated(object sender, QRCodeUpdatedEventArgs args) => OnQRCodeUpdated?.Invoke(sender, args.Code);
        private void QRCodeWatcher_Removed(object sender, QRCodeRemovedEventArgs args) => OnQRCodeRemoved?.Invoke(sender, args.Code);
        private void QRCodeWatcher_EnumerationCompleted(object sender, object e) => Debug.Log($"Enum:{e}");
    }
}