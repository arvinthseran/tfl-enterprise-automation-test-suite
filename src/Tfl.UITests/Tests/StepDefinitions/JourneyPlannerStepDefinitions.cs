
using Tfl.UITests.Support;
using Tfl.UITests.Tests.Pages.JourneyPlanner;

namespace Tfl.JourneyPlannerTests.StepDefinitions;


[Binding]
public sealed class JourneyPlannerStepDefinitions
{
    private readonly ScenarioContext _context;

    private readonly DataHelpers _dataHelpers;

    public (string from, string to) _journeyDetails;

    private PlanAJourneyPage _planAJourneyPage;
    private JourneyResultsPage _journeyResultsPage;

    public JourneyPlannerStepDefinitions(ScenarioContext context) 
    {
        _context = context; 
        _dataHelpers = context.Get<DataHelpers>();
    }

    [Given(@"the user enters a valid locations")]
    public void GivenTheUserEntersAValidLocations() => GoToPlanAJourneyPage(_dataHelpers.ValidJourneys[0]);

    [Given(@"the user enters an invalid locations")]
    public void GivenTheUserEntersAnValidLocations() => GoToPlanAJourneyPage(_dataHelpers.InValidJourney);

    [Given(@"the user enters a multiple locations")]
    public void GivenTheUserEntersAMultipleLocations() => GoToPlanAJourneyPage(_dataHelpers.MultipleLocation);

    [Given(@"the user does not have a location")]
    public void GivenTheUserDoesNotHaveALocations() => GoToPlanAJourneyPage(_dataHelpers.EmptyLocation);

    [When(@"the user plans for multiple journeys")]
    public void WhenTheUserPlansForMultipleJourneys()
    {
        AcceptCookiesAndGoToPlanAJourneyPage();

        foreach (var (from, to) in _dataHelpers.ValidJourneys)
        {
            _journeyResultsPage = _planAJourneyPage.UserPlansAJourneyFromSuggestions(from, to);

            VerifyValidJourneyResults();

            _planAJourneyPage = _journeyResultsPage.GoToPlanAJourneyPage();
        }
    }


    [When(@"the user plan a journey with no locations")]
    public void WhenTheUserPlanAJourneyWithNoLocations() => _planAJourneyPage = _planAJourneyPage.UserPlansAJourneyWithNoLocations();

    [When(@"the user plan a journey")]
    public void WhenTheUserPlanAJourney() => _journeyResultsPage  = _planAJourneyPage.UserPlansAJourney(_journeyDetails.from, _journeyDetails.to);

    [When(@"the user plan a journey based on arrival time")]
    public void WhenTheUserPlanAJourneyBasedOnArrivalTime() => _journeyResultsPage = _planAJourneyPage.UserPlansAJourneyBasedOnArrival(_journeyDetails.from, _journeyDetails.to);

    [Then(@"the user should see more than one matching location")]
    public void ThenTheUserShouldSeeMoreThanOneMatchingLocation() 
        => StringAssert.Contains("We found more than one location matching", _journeyResultsPage.GetDisambiguationMessage());

    [Then(@"the user should see valid journey results")]
    public void ThenTheUserShouldSeeValidJourneyResults() => VerifyValidJourneyResults();

    [Then(@"should not see more than one matching location")]
    public void ThenShouldNotSeeMoreThanOneMatchingLocation() => Assert.AreEqual(false, _journeyResultsPage.IsDisambiguationMessageDisplayed(), "More than one matching location found");

    [Then(@"the user shouldn't find matching journey results")]
    public void ThenTheUserShouldntFindMatchingJourneyResults() 
        => StringAssert.Contains("we can't find a journey matching your criteria", _journeyResultsPage.GetFieldValidationErrors());

    [Then(@"the user is unable to plan a journey")]
    public void ThenTheUserIsUnableToPlanAJourney()
    {
        Assert.Multiple(() =>
        {
            StringAssert.Contains("The From field is required.", _planAJourneyPage.GetInputFromError());
            StringAssert.Contains("The To field is required.", _planAJourneyPage.GetInputToError());
        });
    }

    [Then(@"the user can edit the journey")]
    public void ThenTheUserCanEditTheJourney() => _journeyResultsPage = _journeyResultsPage.EditJourney();

    [Then(@"the recent journeys can be found in the recent tab")]
    public void ThenTheRecentJourneyCanBeFoundInTheRecentTab() => Assert.AreEqual(true, _planAJourneyPage.IsRecentJourneyItemDisplayed(), "Recent journey is not found");

    private void VerifyValidJourneyResults() => StringAssert.Contains("Fastest by public transport", _journeyResultsPage.GetJourneyResults());

    private PlanAJourneyPage GoToPlanAJourneyPage((string from, string to) journeyDetails)
    {
        _journeyDetails = journeyDetails;

        return AcceptCookiesAndGoToPlanAJourneyPage();
    }

    private PlanAJourneyPage AcceptCookiesAndGoToPlanAJourneyPage() => _planAJourneyPage = new LandingPage(_context).AcceptCookiesAndGoToPlanAJourneyPage();
}