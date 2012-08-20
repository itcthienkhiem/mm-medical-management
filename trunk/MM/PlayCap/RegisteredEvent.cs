using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace PlayCap
{
    public class RegisteredEvent
    {
        public List<IPlayCapCallback> Callbacks = new List<IPlayCapCallback>();

        public void Register(IPlayCapCallback callback)
        {
            if (!Callbacks.Contains(callback))
            {
                Callbacks.Add(callback);

                //check to close
                ICommunicationObject obj = callback as ICommunicationObject;
                obj.Closed += new EventHandler(obj_Closed);
            }
        }

        void obj_Closed(object sender, EventArgs e)
        {
            if (sender is IPlayCapCallback)
            {
                Unregister(sender as IPlayCapCallback);
            }
        }

        void Unregister(IPlayCapCallback callback)
        {
            if (Callbacks.Contains(callback))
                Callbacks.Remove(callback);
        }

        public void RaiseOnBitmap(byte[] bmp)
        {
            Callbacks.ForEach(delegate(IPlayCapCallback callback)
            {
                if (callback != null)
                    callback.OnBitmap(bmp);
            });
        }
    }
}
