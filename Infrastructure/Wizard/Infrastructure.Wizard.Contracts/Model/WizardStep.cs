namespace Infrastructure.Wizard.Contracts.Model
{
    public class WizardStep
    {
        public string StepName { get; set; }
        public string Description { get; set; }
        public int StepOrder { get; set; }
        public string ViewTargetName { get; set; }
    }
}