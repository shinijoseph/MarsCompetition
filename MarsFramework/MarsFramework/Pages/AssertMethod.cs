using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsFramework.Pages
{
    class AssertMethod
    {
        public AssertMethod()
        {
            PageFactory.Equals(Global.GlobalDefinitions.driver, this);
        }

        internal bool Assert_Method(object expectedResult, object actualResult, string message)
        {

            if (actualResult.Equals(expectedResult))
            {
                Global.Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Pass, $"Test Passed, {message} successfull");
                return true;
            }
            else
            {
                Global.Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Fail, $"Test Failed, {message} Unsucessfull");
                return false;
            }
        }

    }
}
