using Godot;
using GodotTemplate.Presentation.Utils;

[SceneReference("CustomPopup.tscn")]
[Tool]
public partial class CustomPopup
{
    [Signal]
    public delegate void PopupClosed();

    private string title;

    [Export]
    public string Title
    {
        get => title;
        set
        {
            if (IsInsideTree())
            {
                this.titleLabel.Text = value;
            }
            title = value;
        }
    }

    private bool showCloseButton = true;

    [Export]
    public bool ShowCloseButton
    {
        get => showCloseButton;
        set
        {
            if (IsInsideTree())
            {
                this.closeButton.Visible = value;
            }
            showCloseButton = value;
        }
    }


    public override void _Ready()
    {
        base._Ready();
        this.FillMembers();

        this.closeButton.Connect(CommonSignals.Pressed, this, nameof(Close));
        Title = title;
        ShowCloseButton = showCloseButton;
#if DEBUG
        this.GetTree().EditedSceneRoot?.SetEditableInstance(this, true);
        this.SetDisplayFolded(true);
#endif
    }

    protected void Close()
    {
        this.Hide();
        this.EmitSignal(nameof(PopupClosed));
    }

    public void ShowAt(Vector2 rectPosition)
    {
        this.customPopupContainer.RectPosition = rectPosition + Vector2.Left * this.customPopupContainer.RectSize / 2;
        this.Show();
    }

    public void ShowCentered()
    {
        var rectPosition = this.GetViewport().Size / 2 / this.Scale;
        this.customPopupContainer.RectPosition = rectPosition - this.customPopupContainer.RectSize / 2;
        this.Show();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);
        if (!Visible)
        {
            return;
        }

        if (@event is InputEventMouse mouse && ((ButtonList)mouse.ButtonMask & ButtonList.Left) == ButtonList.Left)
        {
            Close();
        }
    }
}
