using TwitterBootstrapMVC.ControlInterfaces;
using TwitterBootstrapMVC.Controls;

namespace Disposable.Web.TwitterBootstrap3.MVC5
{
    public static class NoLabelExtensions
    {
        public static IBootstrapLabel NoLabel<TModel>(this BootstrapControlGroupActionLink<TModel> control)
        {
            return control == null ? null : control.Label().Class("sr-only");
        }

        public static IBootstrapLabel NoLabel<TModel>(this BootstrapControlGroupActionLinkButton<TModel> control)
        {
            return control == null ? null : control.Label().Class("sr-only");
        }

        public static IBootstrapLabel NoLabel<TModel>(this BootstrapControlGroupCheckBox<TModel> control)
        {
            return control == null ? null : control.Label().Class("sr-only");
        }

        public static IBootstrapLabel NoLabel<TModel>(this BootstrapControlGroupDisplay<TModel> control)
        {
            return control == null ? null : control.Label().Class("sr-only");
        }

        public static IBootstrapLabel NoLabel<TModel>(this BootstrapControlGroupDisplayText<TModel> control)
        {
            return control == null ? null : control.Label().Class("sr-only");
        }

        public static IBootstrapLabel NoLabel<TModel>(this BootstrapControlGroupDropDownList<TModel> control)
        {
            return control == null ? null : control.Label().Class("sr-only");
        }

        public static IBootstrapLabel NoLabel<TModel>(this BootstrapControlGroupDropDownListFromEnum<TModel> control)
        {
            return control == null ? null : control.Label().Class("sr-only");
        }

        public static IBootstrapLabel NoLabel<TModel>(this BootstrapControlGroupEditor<TModel> control)
        {
            return control == null ? null : control.Label().Class("sr-only");
        }

        public static IBootstrapLabel NoLabel<TModel>(this BootstrapControlGroupFile<TModel> control)
        {
            return control == null ? null : control.Label().Class("sr-only");
        }

        public static IBootstrapLabel NoLabel<TModel, TSourse, TValue, TText>(this BootstrapControlGroupInputList<TModel, TSourse, TValue, TText> control)
        {
            return control == null ? null : control.Label().Class("sr-only");
        }

        public static IBootstrapLabel NoLabel<TModel>(this BootstrapControlGroupInputListFromEnum<TModel> control)
        {
            return control == null ? null : control.Label().Class("sr-only");
        }

        public static IBootstrapLabel NoLabel<TModel>(this BootstrapControlGroupLink<TModel> control)
        {
            return control == null ? null : control.Label().Class("sr-only");
        }

        public static IBootstrapLabel NoLabel<TModel>(this BootstrapControlGroupListBox<TModel> control)
        {
            return control == null ? null : control.Label().Class("sr-only");
        }

        public static IBootstrapLabel NoLabel<TModel>(this BootstrapControlGroupPassword<TModel> control)
        {
            return control == null ? null : control.Label().Class("sr-only");
        }

        public static IBootstrapLabel NoLabel<TModel>(this BootstrapControlGroupRadioButton<TModel> control)
        {
            return control == null ? null : control.Label().Class("sr-only");
        }

        public static IBootstrapLabel NoLabel<TModel>(this BootstrapControlGroupRadioButtonTrueFalse<TModel> control)
        {
            return control == null ? null : control.Label().Class("sr-only");
        }

        public static IBootstrapLabel NoLabel<TModel>(this BootstrapControlGroupText<TModel> control)
        {
            return control == null ? null : control.Label().Class("sr-only");
        }

        public static IBootstrapLabel NoLabel<TModel>(this BootstrapControlGroupTextArea<TModel> control)
        {
            return control == null ? null : control.Label().Class("sr-only");
        }

        public static IBootstrapLabel NoLabel<TModel>(this BootstrapControlGroupTextBox<TModel> control)
        {
            return control == null ? null : control.Label().Class("sr-only");
        }
    }
}
