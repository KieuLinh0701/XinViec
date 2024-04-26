using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XinViec
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace XinViec
    {
        public class StateStorage
        {
            private static StateStorage instance;
            public string SharedValue { get; set; }

            private StateStorage() { }

            public static StateStorage GetInstance()
            {
                if (instance == null)
                {
                    instance = new StateStorage();
                }
                return instance;
            }
        }
    }

}
