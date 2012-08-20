using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PlayCap
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class PlayCap : IPlayCap
    {
        public void RegisterEvent()
        {
            PlayCapFactory.PlayCapFact.ClientEvent.Register(OperationContext.Current.GetCallbackChannel<IPlayCapCallback>());
        }

        public void Capture()
        {
            PlayCapFactory.PlayCapFact.Capture();
        }
    }
}
