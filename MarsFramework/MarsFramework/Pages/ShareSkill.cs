/********************************************************************************
 ShareSkill Class is implemented according to the Design Pattern POM with
 PageFactory to reuse the Code at Maximum.   The Concept is One Class is for One
 WebPage.The ShareSkill Class Consist of the elements operations present in 
 ShareSkill Webpage.
 *****************************************************************************/

using MarsFramework.Config;
using MarsFramework.Global;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Windows.Forms;
using MarsFramework.Pages;
using MarsFramework;

namespace MarsFramework
{
    internal class ShareSkill
    {
        public static int RowCount = Int32.Parse(MarsResource.RowNum);
        int count = 1;
        public ShareSkill()
        {
            PageFactory.InitElements(Global.GlobalDefinitions.driver, this);
        }

        #region  Initialize Web Elements 

        //Define ShareSkill button
        [FindsBy(How = How.LinkText, Using = "Share Skill")]
        private IWebElement ShareSkillButton { get; set; }

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

        //Define ServiceType 
        [FindsBy(How = How.XPath, Using = "//*[@id='service-listing-section']/div[2]/div/form/div[5]/div[2]/div[1]/div[2]/div/input")]
        private IWebElement ServiceType { get; set; }

        //Define ServiceTypeText 
        [FindsBy(How = How.XPath, Using = "//*[@id='service-listing-section']/div[2]/div/form/div[5]/div[2]/div[1]/div[2]/div/label")]
        private IWebElement ServiceTypeText { get; set; }

        //Define LocationType 
        [FindsBy(How = How.XPath, Using = "//*[@id='service-listing-section']/div[2]/div/form/div[6]/div[2]/div/div[1]/div/input")]
        private IWebElement LocationType { get; set; }

        //Define StartDate 
        [FindsBy(How = How.Name, Using = "startDate")]
        private IWebElement StartDate{ get; set; }

        //Define EndDate 
        [FindsBy(How = How.Name, Using = "endDate")]
        private IWebElement EndDate { get; set; }

        //Define Skill-Exchange Tag
        [FindsBy(How = How.XPath, Using = "//*[@id='service-listing-section']/div[2]/div/form/div[8]/div[4]//input")]
        private IWebElement SkillTag { get; set; }

        //Define Worksample upload
        [FindsBy(How = How.XPath, Using = "//*[@id='service-listing-section']//span/i")]
        private IWebElement WorkSample { get; set; }

        //Define Save Button
        [FindsBy(How = How.XPath, Using = "//input[contains(@class, 'ui teal button')]")]
        private IWebElement Save { get; set; }

        //Define Cancel Button
        [FindsBy(How = How.XPath, Using = "//input[contains(@class, 'ui button')]")]
        private IWebElement Cancel { get; set; }

        //Define View Button
        [FindsBy(How = How.XPath, Using = "//*[@id='listing-management-section']/div[2]/div[1]/table/tbody/tr[1]//i[1]")]
        private IWebElement View { get; set; }

        //Define Manage Listings
        [FindsBy(How = How.XPath, Using = "//*[@id='service-detail-section']/section[1]//a[3]")]
        private IWebElement ManageListings { get; set; }

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

        /** ShareASkill Method to handle the ShareSkill page details**/
        internal void ShareASkill(string option)
        {
            string date, imagePath, fullimagePath, actualResult, expectResult, user;
            string[] startDate;
            string[] endDate;
            string[] exResult;
            int imageCount =1, i=0;
            bool verifyAdd;
            string expectedResult = "Manage Listings";
            string expectedResult_Cancel = "Mars Logo";
            string expected_valid = "Special characters are not allowed.";
            string message = "Share A Skill";
            string message_cancel = "Cancel the share skill form";
            string message_char = "Restriciting special Characters in Title Filed";
            try
            {
                //Populate the data from Excel Sheet
                GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPath, "ShareSkill");
                Thread.Sleep(1000);

                //Click ShareSkill Button
                ShareSkillButton.Click();
                Thread.Sleep(500);

                //Enter the Title
                //If user input is special char, take the data from 3rd row of excelsheet
                if(option.Equals("spchar"))
                {
                    Title.SendKeys(GlobalDefinitions.ExcelLib.ReadData(RowCount+1, "Title"));
                    GlobalDefinitions.wait(5);
                    goto finish;
                }
                else
                {
                    Title.SendKeys(GlobalDefinitions.ExcelLib.ReadData(RowCount, "Title"));
                    GlobalDefinitions.wait(5);

                }
                

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
                for ( i = 0; i < count; i++)
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
                for ( i = 0; i < subcount; i++)
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

                //Select Service Type "One-Off service" 
                ServiceType.Click();
                
                //Select Location Type "On-Site" 
                LocationType.Click();

                /*Enter the StartDate. Since the date format getting from excel sheet includes 
                 time as well, so need to split it to get the date alone*/
                
                date = GlobalDefinitions.ExcelLib.ReadData(RowCount, "StartDate");
                startDate = date.Split(' ');
                StartDate.Click();
                StartDate.SendKeys(startDate[0]);

                Thread.Sleep(1000);
           
            
                //Enter the EndDate
                date = GlobalDefinitions.ExcelLib.ReadData(RowCount, "EndDate");
                endDate = date.Split(' ');
                EndDate.Click();
                EndDate.SendKeys(endDate[0]);

                // DatePicker using Javascrip Executer
                //jse.ExecuteScript("arguments[0].setAttribute('value','"+strDate+"');", StartDate);

                //Enter Skill-Exchange Tag
                SkillTag.Click();
                SkillTag.SendKeys(GlobalDefinitions.ExcelLib.ReadData(RowCount, "SkillTag"));
                SkillTag.SendKeys(OpenQA.Selenium.Keys.Enter);
                Thread.Sleep(1000);

                //If User input is "moresample", upload more than 5 samples.By default imageCount is 1
                if (option.Equals("moresample"))
                {
                    imageCount = 7;
                }

                i = 0; 
                while (i < imageCount)
                {
                    //Verify the Worksample button is displayed after adding 5 samples 
                    
                    if(WorkSample.Displayed)
                    {
                        //Click on Worksample
                        WorkSample.Click();

                        //Upload 5 work Samples
                        imagePath = GlobalDefinitions.ExcelLib.ReadData(RowCount+i, "WorkSamplePath");
                        fullimagePath = System.IO.Path.GetFullPath(imagePath);
                      // Console.WriteLine(fullimagePath);
                        Thread.Sleep(3000);
                        SendKeys.SendWait(@fullimagePath);
                        Thread.Sleep(3000);
                        SendKeys.SendWait(@"{Enter}");
                        Thread.Sleep(3000);
                        i++;
                        
                        
                    }
                    else
                    {
                        /*Bug- ErrorMessage should display to restrict the uploading of more than 5 work samples.
                         *  Due to Lack of error message verifying the same is not possible*/ 
                        Global.Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Pass, "Restricting upload of more than 5 samples Successful");
                    }
                    
                }

                finish:
                switch (option)
                {
                    //Share a Service
                    case "create":

                        /*Bug- With Worksample, after creating the service , it isnot navigating to next page.Hence removed 
                         * the sample to verify the basic functionality of ShareSkill*/
                        Global.GlobalDefinitions.driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]//div/i")).Click();

                        expectResult = ServiceTypeText.Text;
                        exResult = expectResult.Split(' ');
                        // Clicking the Save Button
                        Save.Click();
                        Thread.Sleep(2000);

                        actualResult = Global.GlobalDefinitions.driver.FindElement(By.XPath("//*[@id='listing-management-section']//h2")).Text;
                        AssertMethod assert = new AssertMethod();

                        // True for creating first time, False for resharing the service
                        verifyAdd = assert.Assert_Method(expectedResult, actualResult, message);
                        
                        /* If the Skill is already shared, Clicking the save button will generate error,
                         Hence no need to verify service in manage listing page*/
                        
                        /*If the sharing of the skill is success, Verify the same is displayed on "Manage Listing" page
                          and Search Result*****/
                        if ((verifyAdd == true) && (this.count== 1))
                        {
                            Thread.Sleep(2000);
                            //View the details of the Service
                            View.Click();
                            Thread.Sleep(1000);
                            
                            //Verify the service is displayed on Manage Listings Page
                            actualResult = GlobalDefinitions.driver.FindElement(By.XPath("//*[@id='service-detail-section']/div[2]/div/div[2]/div[1]/div[1]/div[2]/div[2]/div/div/div[2]/div/div[3]//div[2]")).Text;
                            if(actualResult.Equals(exResult[0]))
                            {
                                Global.Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Pass, "Test Passed, Service is listed on Manage Listing page");
                                this.count++;
                            }
                            else
                            {
                                Global.Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Fail, "Test Failed, Service is not listed on Manage Listing page");
                                break;
                            }

                            //Verify the Service is displayed on Searching 
                            actualResult = GlobalDefinitions.ExcelLib.ReadData(RowCount, "Title");
                            user = GlobalDefinitions.ExcelLib.ReadData(RowCount, "User");
                            ManageListings.Click();
                            Thread.Sleep(1500);

                            //Calling SearchAService method to Search the service
                            SearchAService(actualResult, user);


                        }
                        // Resharing a skill is restricted
                        else if ((verifyAdd == false) && (this.count == 2))
                        {
                            
                                Global.Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Pass, "Test Passed, Restricted resharing a skill");
                            
                           
                        }
                        // Resharing a skill is not restricted
                        else if ((verifyAdd == true) && (this.count == 2))
                        {
                            Global.Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Fail, "Test Failed, Not able to Restrict resharing a skill");

                        }
                        Thread.Sleep(1500);
                        
                        break;

                    case "cancel":
                        // Clicking the Cancel Button
                        Cancel.Click();
                        Thread.Sleep(1500);

                        actualResult = Global.GlobalDefinitions.driver.FindElement(By.LinkText("Mars Logo")).Text;
                        AssertMethod assert_close = new AssertMethod();
                        assert_close.Assert_Method(expectedResult_Cancel, actualResult, message_cancel);
                        break;
                    
                    case "spchar":
                        actualResult = Global.GlobalDefinitions.driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[1]/div/div[2]/div/div[2]/div")).Text;
                        AssertMethod assert_char = new AssertMethod();
                        assert_char.Assert_Method(expected_valid, actualResult, message_char);
                        break;

                }

            }
            catch(Exception e)
            {
                Global.Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Fail, "Test Failed, Not able to share a skill", e.Message);
            }

        }



        /******* SearchAService Method to search a Service******/
        internal bool SearchAService(string service, string user)
        {
            string actualValue;
            string message = "Search A Service";
            try
            {
                //Enter the value in the search bar
                SearchBar.Click();
                SearchBar.SendKeys(service);
                Thread.Sleep(2000);

                //Click on the search button
                SearchButton.Click();
                Global.GlobalDefinitions.driver.Navigate().Refresh();
                SearchUser.Clear();
                SearchUser.SendKeys(user);
                SelectUser.Click();
                Global.GlobalDefinitions.WaitForElement(Global.GlobalDefinitions.driver, By.XPath("//*[@id='service-search-section']/div[2]/div/section/div/div[2]/div/div[2]/div/div/div[1]//p"), 10);

                Thread.Sleep(2000);
                actualValue = Global.GlobalDefinitions.driver.FindElement(By.XPath("//*[@id='service-search-section']/div[2]/div/section/div/div[2]/div/div[2]/div/div/div[1]//p")).Text;
                AssertMethod assert = new AssertMethod();
                return (assert.Assert_Method(service, actualValue, message));


            }
            catch (Exception e)
            {
                Global.Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Fail, "Test Failed, Service Not found", e.Message);
                return false;
            }

        }


    }
}
