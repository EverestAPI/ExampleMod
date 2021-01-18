// https://github.com/EverestAPI/Resources/wiki/TextMenu-Reference

using Celeste.Mod.UI;
using System.Collections.Generic;

namespace Celeste.Mod.Example {
    public class ExampleOui : OuiGenericMenu, OuiModOptions.ISubmenu {
        public override string MenuName => "Example Menu";

        protected override void addOptionsToMenu(TextMenu menu) {
            menu.Add(new TextMenu.Header("Example Header"));

            menu.Add(new TextMenu.Button("This Does Nothing"));

            menu.Add(new TextMenu.OnOff("Toggle", false));

            menu.Add(new TextMenu.Slider("Range", i => new string[] { "One", "Two", "Three" }[i], 0, 2));

            menu.Add(new TextMenu.SubHeader("That's the end of the vanilla items"));

            menu.Add(new TextMenuExt.IntSlider("This one handles big values", 0, 2000));

            // Adding submenus can be quite verbose, so they get their own functions
            addSubMenu(menu);
            addOptionSubMenu(menu);

            menu.Add(new TextMenuExt.HeaderImage("menu/everest"));
        }

        private void addSubMenu(TextMenu menu) {
            menu.Add(new TextMenuExt.SubMenu("Click Me!", enterOnSelect: false));
        }

        private void addOptionSubMenu(TextMenu menu) {

            menu.Add(new TextMenuExt.OptionSubMenu("Toggle Me!").Add(
                "Empty", null)
                .Add("Filled", new List<TextMenu.Item> {
                    new TextMenu.Button("Btn"),
                    new TextMenu.OnOff("OnOff", false)
                })
            );
        }

    }
}
