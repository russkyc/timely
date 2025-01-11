using MudBlazor;

namespace Russkyc.Timely.Utilities;

public static class AppTheme
{
    public static readonly MudTheme BaseTheme = new ()
    {
        PaletteLight = new PaletteLight()
        {
            AppbarBackground = "#ffffff",
            Background = "#f5fafa",
            Primary = "#19787c",
            Surface = "#ffffff",
            Secondary = "#bce0b4",
            Info = "#176f71",
            Success = "#192f6f",
            Error = "#FF2950"
        },
        PaletteDark = new PaletteDark()
        {
            AppbarBackground = "#014344",
            DrawerBackground = "#161a25",
            BackgroundGray = "#0d4c4d",
            Surface = "#1a5556",
            Background = "#014344",
            Primary = "#19787c",
            Secondary = "#a4d6b7",
            Info = "#192f6f",
            Success = "#192f6f"
        },
        LayoutProperties = new LayoutProperties()
        {
            DefaultBorderRadius = ".4rem"
        }
    };
}