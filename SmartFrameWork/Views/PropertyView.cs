using System;

namespace SmartFrameWork.Views
{
    public partial class PropertyView : FrameWorkView
    {
        public static readonly string NAME = "Property";

        public static readonly string ICON = "property.png";

        public PropertyView()
        {
            InitializeComponent();
            ToolBarVisible = false;
            SelectionManager.SelectionChanged += new SelectionManager.SelectionChange(SelectionManager_SelectionChanged);
        }

        public override string Icon
        {
            get
            {
                return ICON;
            }
        }

        void SelectionManager_SelectionChanged(object selection)
        {
            this.propertyGrid.SelectedObject = selection;
        }

        private void propertyGrid_SelectedObjectsChanged(object sender, EventArgs e)
        {
            if (SelectionManager.Selection != this.propertyGrid.SelectedObject)
            {
                SelectionManager.Selection = this.propertyGrid.SelectedObject;
            }
        }

        public override void Close()
        {
            this.propertyGrid.SelectedObject = null;
            base.Close();
        }
    }

    public class PropertyViewCreator : IViewCreator
    {
        public string Name
        {
            get { return PropertyView.NAME; }
        }

        public string Icon
        {
            get { return PropertyView.ICON; }
        }

        public FrameWorkView CreateView(FrameWorkWindow window)
        {
            PropertyView view = new PropertyView();
            return view;
        }
    }
}
