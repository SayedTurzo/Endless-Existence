namespace EndlessExistence.Item_Interaction.Scripts.ObjectScripts
{
    public interface EE_IObject
    { 
        string Description { get; set; }
        float Value { get; set; }
        bool HaveDescription { get; set; }

        void SetDescription();

        void Interact();
    }
}
