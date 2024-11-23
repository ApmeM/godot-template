using Godot;

[SceneReference("CustomTextPopup.tscn")]
[Tool]
public partial class CustomTextPopup
{
    private string content;

    [Export(PropertyHint.MultilineText)]
    public string ContentText
    {
        get => this.content;
        set
        {
            if (IsInsideTree())
            {
                this.contentText.Text = value;
            }
            this.content = value;
        }
    }

    public override void _Ready()
    {
        base._Ready();
        this.FillMembers();

        this.ContentText = content;
    }
}
