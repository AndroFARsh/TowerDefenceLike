using Smooth.Delegates;

namespace Smooth.Algebraics
{
    public static class OptionsExtentions
    {
        public static Option<T> Do4Each<T>(this Option<T> option, DelegateAction<T> action)
        {
            option.ForEach(action);
            return option;
        }
    }
}