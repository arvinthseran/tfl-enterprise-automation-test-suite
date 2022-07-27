namespace SpecFlowProject1.StepDefinitions;

[Binding]
public sealed class JourneyPlannerStepDefinitions
{
    // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

    [Given(@"the user wants to travel from '([^']*)' to '([^']*)'")]
    public void GivenTheUserWantsToTravelFromTo(string from, string to)
    {
        throw new PendingStepException();
    }

    [When(@"the user plan a journey")]
    public void WhenTheUserPlanAJourney()
    {
        throw new PendingStepException();
    }

    [Then(@"the user should see valid journey results")]
    public void ThenTheUserShouldSeeValidJourneyResults()
    {
        throw new PendingStepException();
    }

}