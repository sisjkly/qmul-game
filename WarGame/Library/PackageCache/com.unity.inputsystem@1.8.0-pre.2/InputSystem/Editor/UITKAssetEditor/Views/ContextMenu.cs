#if UNITY_EDITOR && UNITY_INPUT_SYSTEM_PROJECT_WIDE_ACTIONS
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UIElements;

namespace UnityEngine.InputSystem.Editor
{
    internal static class ContextMenu
    {
        private static readonly string rename_String = "Rename";
        private static readonly string duplicate_String = "Duplicate";
        private static readonly string delete_String = "Delete";

        private static readonly string add_Action_String = "Add Action";
        private static readonly string add_Binding_String = "Add Binding";
        public static void GetContextMenuForActionMapItem(InputActionMapsTreeViewItem treeViewItem)
        {
            var _ = new ContextualMenuManipulator(menuEvent =>
            {
                menuEvent.menu.AppendAction(add_Action_String, _ => InputActionViewsControlsHolder.CreateActionMap.Invoke(treeViewItem));
                menuEvent.menu.AppendSeparator();
                menuEvent.menu.AppendAction(rename_String, _ => InputActionViewsControlsHolder.RenameActionMap.Invoke(treeViewItem));
                AppendDuplicateActionMap(menuEvent, treeViewItem);
                AppendDeleteActionMap(menuEvent, treeViewItem);
            }) { target = treeViewItem };
        }

        public static void GetContextMenuForActionItem(InputActionsTreeViewItem treeViewItem, string controlLayout, int index)
        {
            var _ = new ContextualMenuManipulator(menuEvent =>
            {
                menuEvent.menu.AppendAction(add_Binding_String, _ => InputActionViewsControlsHolder.AddBinding.Invoke(treeViewItem));
                AppendCompositeMenuItems(treeViewItem, controlLayout, menuEvent.menu);
                menuEvent.menu.AppendSeparator();
                AppendRenameAction(menuEvent, index, treeViewItem);
                AppendDuplicateAction(menuEvent, treeViewItem);
                AppendDeleteAction(menuEvent, treeViewItem);
            }) { target = treeViewItem };
        }

        private static void AppendCompositeMenuItems(InputActionsTreeViewItem treeViewItem, string expectedControlLayout, DropdownMenu menu)
        {
            foreach (var compositeName in InputBindingComposite.s_Composites.internedNames.Where(x =>
                !InputBindingComposite.s_Composites.aliases.Contains(x)).OrderBy(x => x))
            {
                // Skip composites we should hide
                var compositeType = InputBindingComposite.s_Composites.LookupTypeRegistration(compositeName);
                var designTimeVisible = compositeType.GetCustomAttribute<DesignTimeVisibleAttribute>();
                if (designTimeVisible != null && !designTimeVisible.Visible)
                    continue;

                // Skip composites that don't match the expected control layout
                if (expectedControlLayout != "Any" && expectedControlLayout != "")
                {
                    var valueType = InputBindingComposite.GetValueType(compositeName);
                    if (valueType != null &&
                        !InputControlLayout.s_Layouts.ValueTypeIsAssignableFrom(
                            new InternedString(expectedControlLayout), valueType))
                        continue;
                }

                var displayName = compositeType.GetCustomAttribute<DisplayNameAttribute>();
                var niceName = displayName != null ? displayName.DisplayName.Replace('/', '\\') : ObjectNames.NicifyVariableName(compositeName) + " Composite";
                menu.AppendAction($"Add {niceName}",  _ => InputActionViewsControlsHolder.AddComposite.Invoke(treeViewItem, compositeName));
            }
        }

        public static void GetContextMenuForCompositeItem(InputActionsTreeViewItem treeViewItem, int index)
        {
            var _ = new ContextualMenuManipulator(menuEvent =>
            {
                AppendRenameAction(menuEvent, index, treeViewItem);
                AppendDuplicateAction(menuEvent, treeViewItem);
                AppendDeleteAction(menuEvent, treeViewItem);
            }) { target = treeViewItem };
        }

        public static void GetContextMenuForBindingItem(InputActionsTreeViewItem treeViewItem)
        {
            var _ = new ContextualMenuManipulator(menuEvent =>
            {
                AppendDuplicateAction(menuEvent, treeViewItem);
                AppendDeleteAction(menuEvent, treeViewItem);
            }) { target = treeViewItem };
        }

        private static void AppendDeleteActionMap(ContextualMenuPopulateEvent menuEvent, InputActionMapsTreeViewItem treeViewItem)
        {
            menuEvent.menu.AppendAction(delete_String, _ => { InputActionViewsControlsHolder.DeleteActionMap.Invoke(treeViewItem); });
        }

        private static void AppendDuplicateActionMap(ContextualMenuPopulateEvent menuEvent, InputActionMapsTreeViewItem treeViewItem)
        {
            menuEvent.menu.AppendAction(duplicate_String, _ => { InputActionViewsControlsHolder.DuplicateActionMap.Invoke(treeViewItem); });
        }

        private static void AppendDeleteAction(ContextualMenuPopulateEvent menuEvent, InputActionsTreeViewItem treeViewItem)
        {
            menuEvent.menu.AppendAction(delete_String, _ => {InputActionViewsControlsHolder.DeleteAction.Invoke(treeViewItem);});
        }

        private static void AppendDuplicateAction(ContextualMenuPopulateEvent menuEvent, InputActionsTreeViewItem treeViewItem)
        {
            menuEvent.menu.AppendAction(duplicate_String, _ => {InputActionViewsControlsHolder.DuplicateAction.Invoke(treeViewItem);});
        }

        private static void AppendRenameAction(ContextualMenuPopulateEvent menuEvent, int index, InputActionsTreeViewItem treeViewItem)
        {
            menuEvent.menu.AppendAction(rename_String, _ => {InputActionViewsControlsHolder.RenameAction.Invoke(index, treeViewItem);});
        }
    }
}
#endif
