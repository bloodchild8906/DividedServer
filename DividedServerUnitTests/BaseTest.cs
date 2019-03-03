
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace DividedServerUnitTests
{
    [TestClass]
    public abstract class BaseTest
    {
        #region Properties
        public TestContext TestContext { get; set; }

        public string Class
        {
            get { return TestContext.FullyQualifiedTestClassName; }
        }
        public string Method
        {
            get { return TestContext.TestName; }
        }
        #endregion

        #region Methods
                
        protected virtual void Trace(string message)
        {
            Debug.WriteLine(message);
        }
        #endregion
    }
}
