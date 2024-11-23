using Godot;
using GodotTemplate.Presentation.Utils;

[SceneReference("CustomConfirmPopup.tscn")]
[Tool]
public partial class CustomConfirmPopup
{
    [Signal]
    public delegate void YesClicked();

    [Signal]
    public delegate void NoClicked();

    [Signal]
    public delegate void ChoiceMade(bool isYes);

    public bool IsLastSelectedYes;

    public override void _Ready()
    {
        base._Ready();
        this.FillMembers();

        this.buttonYes.Connect(CommonSignals.Pressed, this, nameof(YesButtonClicked));
        this.buttonNo.Connect(CommonSignals.Pressed, this, nameof(NoButtonClicked));

        this.ShowCloseButton = false;
    }

    private void NoButtonClicked()
    {
        IsLastSelectedYes = false;
        this.EmitSignal(nameof(NoClicked));
        this.EmitSignal(nameof(ChoiceMade), IsLastSelectedYes);
        this.Close();
    }

    private void YesButtonClicked()
    {
        IsLastSelectedYes = true;
        this.EmitSignal(nameof(YesClicked));
        this.EmitSignal(nameof(ChoiceMade), IsLastSelectedYes);
        this.Close();
    }
}
