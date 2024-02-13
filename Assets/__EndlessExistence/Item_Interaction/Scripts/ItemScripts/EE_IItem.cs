namespace __EndlessExistence.Item_Interaction.Scripts.ItemScripts
{
    public interface EE_IItem
    { 
        string Description { get; set; }
        float Value { get; set; }
        bool HaveDescription { get; set; }

        void SetDescription();
    }
}
