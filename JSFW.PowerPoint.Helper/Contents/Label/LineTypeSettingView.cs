using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSFW.PowerPoint.Helper.Contents.Label
{
    public partial class LineTypeSettingView : UserControl
    {
        ShapeType _ShapeType = ShapeType.NoLine;
        public ShapeType ShapeType
        {
            get { return _ShapeType; }
            set
            {
                _ShapeType = value;
                OnValueChanged();
            }
        }

        float _Weight = 1f;

        public float Weight { get { return _Weight; } set { _Weight = value; OnValueChanged(); } }

        private void OnValueChanged()
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler ValueChanged = null;

        public LineTypeSettingView()
        {
            InitializeComponent();
        }

        private void cboFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strWeight = cboFontSize.Text;
            if (string.IsNullOrWhiteSpace(strWeight)) {
                strWeight = "1";
            }
             
            if(!float.TryParse( strWeight, out _Weight) ) {
                Weight = 1f;
            }
            Weight = _Weight;
            rdoShapeType1.BorderWeight = Weight;
            rdoShapeType2.BorderWeight = Weight;
            rdoShapeType3.BorderWeight = Weight;
        }

        private void rdoShapeType1_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoShapeType1.Checked) {
                ShapeType = ShapeType.NoLine;
            }
        }

        private void rdoShapeType2_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoShapeType2.Checked)
            {
                ShapeType = ShapeType.Box;
            }
        }

        private void rdoShapeType3_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoShapeType3.Checked)
            {
                ShapeType = ShapeType.RoundBox;
            }
        }
    }
}
