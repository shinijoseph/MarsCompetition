using MarsFramework.Config;
using MarsFramework.Global;
using MarsFramework.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MarsFramework
{
    class ManageListings
    {
        public static int RowCount = Int32.Parse(MarsResource.RowNum);
        public ManageListings()
        {
            PageFactory.InitElements(Global.GlobalDefinitions.driver, this);
        }

        #region  Initialize Web Elements 

        //Define Edit button
        [FindsBy(How = How.XPath, Using = "//*[@id='listing-management-section']/div[2]/div[1]/table/tbody/tr[1]//i[2]")]
        private IWebElement EditButton { get; set; }

        //Define Title Field
        [FindsBy(How = How.Name, Using = "title")]
        private IWebElement Title { get; set; }

        //Define Description Field
        [FindsBy(How = How.Name, Using = "description")]
        private IWebElement Description { get; set; }

        //Define Category Dropdown Field
        [FindsBy(How = How.XPath, Using = "//*[@id='service-listing-section']/div[2]/div/form/div[3]/div[2]/div/div/select")]
        private IWebElement CategoryDrop { get; set; }

        //Define SubCategory 
        [FindsBy(How = How.Name, Using = "subcategoryId")]
        private IWebElement Subcategory { get; set; }

        //Define Tags 
        [FindsBy(How = How.ClassName, Using = "ReactTags__tagInputField")]
        private IWebElement Tags { get; set; }

        //Define StartDate 
        [FindsBy(How = How.Name, Using = "startDate")]
        private IWebElement StartDate { get; set; }

        //Define Skill-Exchange Tag
        [FindsBy(How = How.XPath, Using = "//*[@id='service-listing-section']/div[2]/div/form/div[8]/div[4]//input")]
        private IWebElement SkillTag { get; set; }

        //Define Active 
        [FindsBy(How = How.XPath, Using = "//*[@id='service-listing-section']/div[2]/div/form/div[10]/div[2]/div/div[2]//input")]
        private IWebElement Active { get; set; }

        //Define Save Button
        [FindsBy(How = How.XPath, Using = "//input[contains(@class, 'ui teal button')]")]
        private IWebElement Save { get; set; }

        //Define Delete Button
        [FindsBy(How = How.XPath, Using = "//*[@id='listing-management-section']/div[2]/div[1]/table/tbody/tr[1]//i[3]")]
        private IWebElement Delete { get; set; }
      
        //Define Confirm Button
        [FindsBy(How = How.XPath, Using = "/html/body/div[2]//button[2]")]
        private IWebElement Confirm { get; set; }

        //Define Service
        [FindsBy(How = How.XPath, Using = "//*[@id='listing-management-section']/div[2]/div[1]/table/tbody/tr[1]/td[3]")]
        private IWebElement Service { get; set; }

        //Define Manage Listings
        [FindsBy(How = How.XPath, Using = "//*[@id='account-profile-section']/div/section[1]/div/a[3]")]
        private IWebElement ManageListingsPage { get; set; }


        //Define SearchBar Button
        [FindsBy(How = How.XPath, Using = "//*[@id='listing-management-section']/div[1]//input")]
        private IWebElement SearchBar { get; set; }

        //Define Search Button
        [FindsBy(How = How.XPath, Using = "//*[@id='listing-management-section']/div[1]/div[1]/i")]
        private IWebElement SearchButton { get; set; }

        //Define Search User
        [FindsBy(How = How.XPath, Using = "//*[@id='service-search-section']/div[2]/div/section/div/div[1]/div[3]//input")]
        private IWebElement SearchUser { get; set; }

        //Define SelectUser
        [FindsBy(How = How.XPath, Using = "//*[@id='service-search-section']/div[2]/div/section/div/div[1]/div[3]/div[1]/div/div[2]/div[1]//span")]
        private IWebElement SelectUser { get; set; }


        #endregion

        /** EditService Method to handle the Editing of a service**/
        internal void EditService()
        {
            int i = 0;
            string date, actualResult;
            string[] startDate;
            string expectedResult = "Manage Listings";
            string message = "Editing of A Service";
            try
            {
                //Populate the data from Excel Sheet
                GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPath, "EditService");
                

                //Navigate to Manage Listings page
                ManageListingsPage.Click();
                Thread.Sleep(1500);

                //Click ShareSkill Button
                EditButton.Click();
                Thread.Sleep(500);

                Title.SendKeys(GlobalDefinitions.ExcelLib.ReadData(RowCount, "Title"));
                GlobalDefinitions.wait(5);

                //Enter the Description
                Description.SendKeys(GlobalDefinitions.ExcelLib.ReadData(RowCount, "Description"));
                GlobalDefinitions.wait(5);

                //Select Category Option
                Thread.Sleep(1000);
                Actions action = new Actions(GlobalDefinitions.driver);
                action.MoveToElement(CategoryDrop).Click().Perform();
                Thread.Sleep(500);

                IList<IWebElement> Categoryselect = CategoryDrop.FindElements(By.TagName("option"));
                int count = Categoryselect.Count;
                for (i = 0; i < count; i++)
                {
                    if (Categoryselect[i].Text == GlobalDefinitions.ExcelLib.ReadData(RowCount, "Category"))
                    {
                        Categoryselect[i].Click();
                        Base.test.Log(LogStatus.Info, "Select the available category");
                        break;

                    }
                }


                //Select SubCategory Option
                Thread.Sleep(1000);
                action.MoveToElement(Subcategory).Click().Perform();
                Thread.Sleep(500);

                IList<IWebElement> SubCategoryselect = Subcategory.FindElements(By.TagName("option"));
                int subcount = SubCategoryselect.Count;
                for (i = 0; i < subcount; i++)
                {
                    if (SubCategoryselect[i].Text == GlobalDefinitions.ExcelLib.ReadData(RowCount, "Subcategory"))
                    {
                        SubCategoryselect[i].Click();
                        Base.test.Log(LogStatus.Info, "Select the available Subcategory");
                        break;

                    }
                }

                Thread.Sleep(1000);
                //Enter Tags
                Tags.SendKeys(GlobalDefinitions.ExcelLib.ReadData(RowCount, "Tags"));
                Tags.SendKeys(OpenQA.Selenium.Keys.Enter);
                Thread.Sleep(1000);

                //Scroll down the webpage
                IJavaScriptExecutor jse = (IJavaScriptExecutor)GlobalDefinitions.driver;
                jse.ExecuteScript("scroll(0, 450)");

           
                /*Enter the StartDate. Since the date format getting from excel sheet includes 
                 time as well, so need to split it to get the date alone*/

                date = GlobalDefinitions.ExcelLib.ReadData(RowCount, "StartDate");
                startDate = date.Split(' ');
                StartDate.Click();
                StartDate.SendKeys(startDate[0]);

                Thread.Sleep(1000);

                //Enter Skill-Exchange Tag
                SkillTag.Click();
                SkillTag.SendKeys(GlobalDefinitions.ExcelLib.ReadData(RowCount, "SkillTag"));
                SkillTag.SendKeys(OpenQA.Selenium.Keys.Enter);
                Thread.Sleep(1000);


                //Change the service to be Hidden
                Active.Click();
                Thread.Sleep(500);

                Save.Click();

                actualResult = Global.GlobalDefinitions.driver.FindElement(By.XPath("//*[@id='listing-management-section']//h2")).Text;
                AssertMethod assert = new AssertMethod();
                if(assert.Assert_Method(expectedResult, actualResult, message))
                {
                    actualResult = Service.Text;
                    expectedResult = GlobalDefinitions.ExcelLib.ReadData(RowCount, "Title");
                    if (actualResult.Equals(expectedResult))
                    {
                        Global.Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Pass, "Test Passed, Edit Successful, Changes are reflected successfully");
                    }
                    else
                    {
                        Global.Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Fail, "Test Failed, Edit UnSuccessful, Changes are not reflected successfully");
                    }
                }

            }
            catch(Exception e)
            {
                Global.Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Fail, "Test Failed, Not able to Edit a Service", e.Message);

            }
        }

        /** DeleteService Method to handle the Deleting of a service**/
        internal void DeleteService()
        {
            string user, actualResult, actualValue;
            try
            {
                //Populate the data from Excel Sheet
                GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPath, "DeleteService");
                Thread.Sleep(1000);

                user = GlobalDefinitions.ExcelLib.ReadData(RowCount, "User");
           
                //Navigate to Manage Listings page
                ManageListingsPage.Click();
                Thread.Sleep(1000);

                actualResult = Service.Text;

                //Click Delete Button
                Delete.Click();
                Thread.Sleep(500);

                //Click on Confirmation Button
                Confirm.Click();
                Thread.Sleep(500);

                //Make sure the service is deleted and not displayed while searching

                //Enter the value in the search bar
                SearchBar.Click();
                SearchBar.SendKeys(actualResult);
                Thread.Sleep(2000);

                //Click on the search button
                SearchButton.Click();
                Global.GlobalDefinitions.driver.Navigate().Refresh();
                Thread.Sleep(1500);
                SearchUser.Clear();
                SearchUser.SendKeys(user);
                Thread.Sleep(500);
                SelectUser.Click();
               // Global.GlobalDefinitions.WaitForElement(Global.GlobalDefinitions.driver, By.XPath("//*[@id='service-search-section']/div[2]/div/section/div/div[2]/div/div[2]/div/div/div[1]//p"), 10);

                Thread.Sleep(1500);
                actualValue = Global.GlobalDefinitions.driver.FindElement(By.XPath("//*[@id='service-search-section']/div[2]/div/section/div/div[2]//h3")).Text;
                
                if(!(actualResult.Equals(actualValue)))
                {
               
                    Global.Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Pass, "Test Passed, Delete Successful, Service is not listed on Searching");
                }
                else
                {
                    Global.Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Fail, "Test Failed, Delete Unsuccessful, Service is listed on Searching");
                }
                


            }
            catch (Exception e)
            {
                Global.Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Fail, "Test Failed, Not able to Delete a Service", e.Message);

            }
        }

    }
}
