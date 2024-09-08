using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BumbleFormula
{
    public partial class MainPage : ContentPage
    {
        private Dictionary<string, double> userFeatures = new Dictionary<string, double>();

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnStartClicked(object sender, EventArgs e)
        {
            // Collect age input
            string ageOption = await DisplayActionSheet("Select Age", "Cancel", null, "18", "19", "20", "21", "22", "23","24","25","26","27","28","29","30","31","32","33","34", "35");
            double age = Convert.ToDouble(ageOption);

            // Collect number of her pictures
            string numberOfPicsOption = await DisplayActionSheet("Number of her Pictures/video, ignore repeated pictures/videos, ignore ones that are not her picture/video, if less than half of her body is showing and her face is not showing too, don't count it also", "Cancel", null, "0", "1", "2", "3","4","5", "6");
            double numberOfPics = Convert.ToDouble(numberOfPicsOption);

            // Collect number of bio lines
            string bioLinesOption = await DisplayActionSheet("Number of Bio Lines, lines containing only smileys and/or only numbers/symbols do not count as bio lines", "Cancel", null, "0", "1", "2", "3", "4", "5", "6", "7", "8","9","10","11","12","13","14","15","16","17");
            double bioLines = Convert.ToDouble(bioLinesOption);

            // Collect height (92 cm to 270 cm)
            string heightOption = await DisplayActionSheet("Select Height (cm), pick 157cm if height is not in profile", "Cancel", null, "92", "93", "94", "95", "96", "97", "98", "99",
"100", "101", "102", "103", "104", "105", "106", "107", "108", "109", "110",
"111", "112", "113", "114", "115", "116", "117", "118", "119", "120",
"121", "122", "123", "124", "125", "126", "127", "128", "129", "130",
"131", "132", "133", "134", "135", "136", "137", "138", "139", "140",
"141", "142", "143", "144", "145", "146", "147", "148", "149", "150",
"151", "152", "153", "154", "155", "156", "157", "158", "159", "160",
"161", "162", "163", "164", "165", "166", "167", "168", "169", "170",
"171", "172", "173", "174", "175", "176", "177", "178", "179", "180",
"181", "182", "183", "184", "185", "186", "187", "188", "189", "190",
"191", "192", "193", "194", "195", "196", "197", "198", "199", "200",
"201", "202", "203", "204", "205", "206", "207", "208", "209", "210",
"211", "212", "213", "214", "215", "216", "217", "218", "219", "220",
"221", "222", "223", "224", "225", "226", "227", "228", "229", "230",
"231", "232", "233", "234", "235", "236", "237", "238", "239", "240",
"241", "242", "243", "244", "245", "246", "247", "248", "249", "250",
"251", "252", "253", "254", "255", "256", "257", "258", "259", "260",
"261", "262", "263", "264", "265", "266", "267", "268", "269", "270");
            double height = Convert.ToDouble(heightOption);

            // Collect physical activity using switch
            string physicalActivityOption = await DisplayActionSheet("How physically active is she?", "Cancel", null, "Almost never", "Sometimes", "Active", "Nil");
            double physicalActivity = physicalActivityOption switch
            {
                "Almost never" => 0,
                "Sometimes" => 1,
                "Active" => 2,
                "Nil" => 1,
                _ => 0
            };

            // Collect education level
            string educationOption = await DisplayActionSheet("Select Education Level", "Cancel", null, "High School", "In college", "Trade/Tech School", "Undergraduate degree", "In grad school", "Graduate degree", "Nil");
            double education = educationOption switch
            {
                "High School" => 1,
                "In college" => 2,
                "Trade/Tech School" => 2.5,
                "Undergraduate degree" => 3,
                "In grad school" => 4,
                "Graduate degree" => 5,
                "Nil" => 3,
                _ => 0
            };

            // Collect drinking habit
            string drinkingOption = await DisplayActionSheet("Select Drinking Habit", "Cancel", null, "No", "Sober", "Rarely", "Sometimes", "Yes", "Nil");
            double drinking = drinkingOption switch
            {
                "No" => 0,
                "Sober" => 1,
                "Rarely" => 2,
                "Sometimes" => 3,
                "Yes" => 4,
                "Nil" => 2,
                _ => 0
            };

            // Collect smoking habit
            string smokingOption = await DisplayActionSheet("Select Smoking Habit", "Cancel", null, "Never", "No", "Sometimes", "Yes", "Nil");
            double smoking = smokingOption switch
            {
                "Never" => 0,
                "No" => 0,
                "Sometimes" => 1,
                "Yes" => 2,
                "Nil" => 1,
                _ => 0
            };

            // Collect desire for children
            string wantChildrenOption = await DisplayActionSheet("Does she want children?", "Cancel", null, "wants kids", "dont want kids", "open to kids", "Not sure", "Nil");
            double wantChildren = wantChildrenOption switch
            {
                "wants kids" => 3,
                "dont want kids" => 0,
                "open to kids" => 2,
                "Not sure" => 1,
                "Nil"  => 1.5,
                _ => 0
            };

            // Collect existing children status with a third option 'nil'
            string haveKidsOption = await DisplayActionSheet("Does she have children?", "Cancel", null, "Have Kids", "Dont have kids", "Nil");
            double haveKids = haveKidsOption switch
            {
                "Have Kids" => 1,
                "Dont have kids" => 0,
                "Nil" => 0.5,
                _ => 0
            };

            // Collect political view
            string politicsOption = await DisplayActionSheet("Select Political View", "Cancel", null, "Liberal", "Moderate", "Conservative", "Apolitical", "Nil");
            double politics = politicsOption switch
            {
                "Liberal" => 1,
                "Moderate" => 2,
                "Conservative" => 3,
                "Apolitical" => 0,
                "Nil" => 1.5,
                _ => 0
            };

            // Collect additional nil information
            int nils = 0;

            // Collect each additional piece of information
            string originOption = await DisplayActionSheet("Select Origin (if not present, nils +1)", "Cancel", null, "Origin present", "Nil");
            if (originOption == "Nil") nils++;

            string occupationOption = await DisplayActionSheet("Select Occupation (if not present, nils +1)", "Cancel", null, "Occupation present", "Nil");
            if (occupationOption == "Nil") nils++;

            string heightOptionAdditional = await DisplayActionSheet("Select Height (if not present, nils +1)", "Cancel", null, "Height present", "Nil");
            if (heightOptionAdditional == "Nil") nils++;

            string physicalActivityOptionAdditional = await DisplayActionSheet("Select Physical Activity (if not present, nils +1)", "Cancel", null, "Physical Activity present", "Nil");
            if (physicalActivityOptionAdditional == "Nil") nils++;

            string educationOptionAdditional = await DisplayActionSheet("Select Education (if not present, nils +1)", "Cancel", null, "Education present", "Nil");
            if (educationOptionAdditional == "Nil") nils++;

            string drinkingOptionAdditional = await DisplayActionSheet("Select Drinking (if not present, nils +1)", "Cancel", null, "Drinking present", "Nil");
            if (drinkingOptionAdditional == "Nil") nils++;

            string smokingOptionAdditional = await DisplayActionSheet("Select Smoking (if not present, nils +1)", "Cancel", null, "Smoking present", "Nil");
            if (smokingOptionAdditional == "Nil") nils++;

            string wantChildrenOptionAdditional = await DisplayActionSheet("Select Want Children (if not present, nils +1)", "Cancel", null, "Want Children present", "Nil");
            if (wantChildrenOptionAdditional == "Nil") nils++;

            string haveKidsOptionAdditional = await DisplayActionSheet("Select Have Kids (if not present, nils +1)", "Cancel", null, "Have Kids present", "Nil");
            if (haveKidsOptionAdditional == "Nil") nils++;

            string starsignOption = await DisplayActionSheet("Select Starsign (if not present, nils +1)", "Cancel", null, "Starsign present", "Nil");
            if (starsignOption == "Nil") nils++;

            string politicsOptionAdditional = await DisplayActionSheet("Select Politics (if not present, nils +1)", "Cancel", null, "Politics present", "Nil");
            if (politicsOptionAdditional == "Nil") nils++;

            string religionOption = await DisplayActionSheet("Select Religion (if not present, nils +1)", "Cancel", null, "Religion present", "Nil");
            if (religionOption == "Nil") nils++;

            string lookingForOption = await DisplayActionSheet("Select Looking For (if not present, nils +1)", "Cancel", null, "Looking For 1st option present", "Nil");
            if (lookingForOption == "Nil") nils++;

            string lookingFor2Option = await DisplayActionSheet("Select Looking For2 (if not present, nils +1)", "Cancel", null, "Looking For 2nd option present", "Nil");
            if (lookingFor2Option == "Nil") nils++;

            string value1Option = await DisplayActionSheet("Select Value1 (if not present, nils +1)", "Cancel", null, "1st Value present", "Nil");
            if (value1Option == "Nil") nils++;

            string value2Option = await DisplayActionSheet("Select Value2 (if not present, nils +1)", "Cancel", null, "2nd Value present", "Nil");
            if (value2Option == "Nil") nils++;

            string value3Option = await DisplayActionSheet("Select Value3 (if not present, nils +1)", "Cancel", null, "3rd Value present", "Nil");
            if (value3Option == "Nil") nils++;

            string cause1Option = await DisplayActionSheet("Select Cause1 (if not present, nils +1)", "Cancel", null, "1st Cause present", "Nil");
            if (cause1Option == "Nil") nils++;

            string cause2Option = await DisplayActionSheet("Select Cause2 (if not present, nils +1)", "Cancel", null, "2nd Cause present", "Nil");
            if (cause2Option == "Nil") nils++;

            string cause3Option = await DisplayActionSheet("Select Cause3 (if not present, nils +1)", "Cancel", null, "3rd Cause present", "Nil");
            if (cause3Option == "Nil") nils++;

            string interest1Option = await DisplayActionSheet("Select Interest1 (if not present, nils +1)", "Cancel", null, "1st Interest present", "Nil");
            if (interest1Option == "Nil") nils++;

            string interest2Option = await DisplayActionSheet("Select Interest2 (if not present, nils +1)", "Cancel", null, "2nd Interest present", "Nil");
            if (interest2Option == "Nil") nils++;

            string interest3Option = await DisplayActionSheet("Select Interest3 (if not present, nils +1)", "Cancel", null, "3rd Interest present", "Nil");
            if (interest3Option == "Nil") nils++;

            // Fill the features into the dictionary
            userFeatures["age"] = age;
            userFeatures["numberofherpictures"] = numberOfPics;
            userFeatures["numberoflinesinbio"] = bioLines;
            userFeatures["height"] = height;
            userFeatures["physicalactivity"] = physicalActivity;
            userFeatures["education"] = education;
            userFeatures["drinking"] = drinking;
            userFeatures["smoking"] = smoking;
            userFeatures["wantchildren"] = wantChildren;
            userFeatures["havekids"] = haveKids;
            userFeatures["politics"] = politics;
            userFeatures["nils"] = nils;

            // Pass the collected values to BumbleFormulaBot
            var result = BumbleFormulaBot.Predict(userFeatures);

            // Display the predictions
            await DisplayAlert("Predictions", result, "OK");
        }
    }
}
