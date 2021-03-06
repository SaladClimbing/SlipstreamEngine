using SlipstreamEngine.Prompts;

namespace SlipstreamEngine.Menus;
public class OptionsMenu
{
    public Window window { get; private set; }
    public MainMenu menu { get; private set; }
    public Action playAction { get; private set; }
    public Prompt prompt { get; private set; } = new Prompt("Options Menu:", new List<string>() { "Font Size" });

    public OptionsMenu(Window window, MainMenu menu, Action playAction)
    {
        this.window = window;
        this.menu = menu;
        this.playAction = playAction;
    }

    public string getMenu() => prompt.getFullString() + "Esc.) Quit";

    public Action optionInput() => () =>
    {
        InputManager.InputManager im = InputManager.InputManager.GetInstance;
        int optionIndex = im.actions.Count - 1;
        switch (im.CheckKey(im.Key))
        {
            case 1:
                {
                    im.SwitchAction(optionIndex, fontControl());
                    this.window.changeString("Font Size: " + this.window.GetConsole().Font.Size + "\nEsc: Exit");
                    break;
                }
            case 2:
            case -2:
                {
                    im.RemoveAction(optionIndex);
                    this.menu.DisplayMenu(this.window, this.playAction);
                    break;
                }
        }
    };

    private Action fontControl() => () =>
    {
        InputManager.InputManager im = InputManager.InputManager.GetInstance;
        int fontIndex = im.actions.Count - 1;
        switch (im.CheckKey(im.Key))
        {
            case 10:
                {
                    this.window.fontSize(this.window.GetConsole().Font.Size + 1f);
                    this.window.changeString("Font Size: " + this.window.GetConsole().Font.Size + "\nEsc: Exit");
                    break;
                }
            case 11:
                {
                    this.window.fontSize(this.window.GetConsole().Font.Size - 1f);
                    this.window.changeString("Font Size: " + this.window.GetConsole().Font.Size + "\nEsc: Exit");
                    break;
                }
            case -2:
                {
                    im.SwitchAction(fontIndex, optionInput());
                    this.window.changeString(getMenu());
                    break;
                }
            default: break;
        }
    };
}
