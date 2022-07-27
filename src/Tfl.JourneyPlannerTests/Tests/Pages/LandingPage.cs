﻿
namespace Tfl.JourneyPlannerTests.Tests.Pages;

public class LandingPage : ProjectBasePage
{
    private static By AcceptCookies => By.CssSelector("#CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll");

    private static By SaveCookiesSettings => By.CssSelector("#cb-confirmedSettings .cb-button");

    public LandingPage(ScenarioContext context) : base(context)
    {

    }

    protected override By PageHeader => By.CssSelector("#cb-cookiebanner .cb-header");

    protected override string PageTitle => "Your cookie settings";

    public PlanAJourneyPage AcceptCookiesAndGoToPlanAJourneyPage()
    {
        driver.FindEnabledAndDisplayedElement(AcceptCookies).Click();
        driver.FindEnabledAndDisplayedElement(SaveCookiesSettings).Click();
        return new(context);
    }
}
