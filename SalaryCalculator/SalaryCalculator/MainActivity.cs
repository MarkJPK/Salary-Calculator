using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace SalaryCalculator
{
  [Activity(Label = "Salary Calculator", MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : Activity
  {
    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      // Set our view from the "main" layout resource
      SetContentView(Resource.Layout.Main);

      // Get our button from the layout resource,
      // and attach an event to it
      EditText grossSalaryText = FindViewById<EditText>(Resource.Id.GrossSalaryText);
      EditText grossPayText = FindViewById<EditText>(Resource.Id.GrossPayText);
      EditText monthlyTaxText = FindViewById<EditText>(Resource.Id.MonthlyTaxText);
      EditText monthlyNationalInsuranceText = FindViewById<EditText>(Resource.Id.MonthlyNationalInsuranceText);
      EditText monthlyPensionText = FindViewById<EditText>(Resource.Id.MonthlyPensionText);
      EditText socialClubText = FindViewById<EditText>(Resource.Id.SocialClubText);
      EditText netPayText = FindViewById<EditText>(Resource.Id.NetPayText);
      Button calculateButton = FindViewById<Button>(Resource.Id.CalculateButton);

      calculateButton.Click += (object sender, EventArgs e) =>
      {
        string grossSalaryString = grossSalaryText.Text;

        double grossSalary = Convert.ToDouble(grossSalaryString);
        double basicPerMonth = grossSalary / 12.0;
        double totalBonus = 0.0;
        double grossPerMonth = basicPerMonth + totalBonus;
        double fixedPensionDeduction = 1800.0;
        double fixedPensionDeductionPerMonth = fixedPensionDeduction / 12.0;
        double contributionEarnings = grossPerMonth - fixedPensionDeductionPerMonth;
        double pensionCost = 9.29 / 100.0;
        double pension = contributionEarnings * pensionCost;
        double taxablePay = grossPerMonth - pension;
        double taxableSalary = taxablePay * 12.0;
        double taxAllowance = 11000.0;
        double netTaxableSalary = taxableSalary - taxAllowance;
        double band1Percentage = 20.0 / 100.0;
        double band2Percentage = 40.0 / 100.0;
        double band1LowerLimit = 0.0;
        double band1UpperLimit = 32000.0;
        double band2LowerLimit = band1UpperLimit + 0.01;
        double band1TaxableAmount = 0.0;
        double band2TaxableAmount = 0.0;
        double band1Tax = 0.0;
        double band2Tax = 0.0;

        if(netTaxableSalary > band1UpperLimit)
        {
          band1TaxableAmount = band1UpperLimit - band1LowerLimit;
        }
        else
        {
          band1TaxableAmount = netTaxableSalary;
        }

        band1Tax = band1TaxableAmount * band1Percentage;

        if(netTaxableSalary - band1TaxableAmount > 0)
        {
          band2TaxableAmount = netTaxableSalary - band1TaxableAmount;
        }
        else
        {
          band2TaxableAmount = 0;
        }

        band2Tax = band2TaxableAmount * band2Percentage;

        double yearlyTax = band1Tax + band2Tax;
        double monthlyTax = yearlyTax / 12.0;

        double nationalInsuranceContributionPay = grossPerMonth - pension;

        double pt = 672.01;
        double ptPercentage = 12.0 / 100.0;
        double ptAmount = 0.0;

        double uel = 3583.01;
        double uelPercentage = 2.0 / 100.0;
        double uelAmount = 0.0;

        if(nationalInsuranceContributionPay >= uel)
        {
          ptAmount = (uel - pt) * ptPercentage;
        }
        else
        {
          ptAmount = (nationalInsuranceContributionPay - pt) * ptPercentage;
        }

        if(nationalInsuranceContributionPay > uel)
        {
          uelAmount = (nationalInsuranceContributionPay - uel) * uelPercentage;
        }
        else
        {
          uelAmount = 0.0;
        }

        double totalNationalInsurance = ptAmount + uelAmount;
        double socialClub = 1.25;

        double monthlyNetPay = grossPerMonth - monthlyTax - totalNationalInsurance - pension - socialClub;

        grossPayText.Text = string.Format(Convert.ToString(grossPerMonth));
        monthlyTaxText.Text = string.Format(Convert.ToString(monthlyTax));
        monthlyNationalInsuranceText.Text = string.Format(Convert.ToString(totalNationalInsurance));
        monthlyPensionText.Text = string.Format(Convert.ToString(pension));
        socialClubText.Text = string.Format(Convert.ToString(socialClub));
        netPayText.Text = string.Format(Convert.ToString(monthlyNetPay));
      };

      var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
      SetActionBar(toolbar);
      ActionBar.Title = "My Toolbar";
    }
  }
}

