using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PlayCap
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IPlayCapCallback))]
    public interface IPlayCap
    {
        [OperationContract(IsOneWay = true)]
        void RegisterEvent();

        [OperationContract(IsOneWay = true)]
        void Capture();
    }

    public interface IPlayCapCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnBitmap(byte[] bmp);
    }
}
