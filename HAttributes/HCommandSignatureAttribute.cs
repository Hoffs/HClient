using System;
using System.Collections.Generic;
using System.Text;
using ChatProtos.Networking;

namespace CoreClient.HAttributes
{
    class HCommandSignatureAttribute : Attribute
    {
        private readonly RequestType _type;

        public HCommandSignatureAttribute(RequestType type)
        {
            _type = type;
        }

        public RequestType GetRequestType()
        {
            return _type;
        }
    }
}
