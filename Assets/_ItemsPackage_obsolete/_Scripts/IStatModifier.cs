// IStatModifier interface
public interface IStatModifier
{
    int InitialHealth { get; }
    int InitialStamina { get; }
    int InitialDefense { get; }

    int CurrentHealth { get; set; }
    int CurrentStamina { get; set; }
    int CurrentDefense { get; set; }

    void ModifyHealth(int amount);
    void ModifyStamina(int amount);
    void ModifyDefense(int amount);
}