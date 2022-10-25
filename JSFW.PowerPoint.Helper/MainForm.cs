using JSFW.PowerPoint.Helper.Contents.Controls;
using JSFW.PowerPoint.Helper.Contents.Label;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSFW.PowerPoint.Helper
{
    public partial class MainForm : Form
    {
        Dictionary<Control, Control> ContentControls = new Dictionary<Control, Control>();

        public MainForm()
        {
            InitializeComponent();

            if (Directory.Exists(PPT_COM_EX.ROOT_CATEGORY_DIR) == false)
            {
                Directory.CreateDirectory(PPT_COM_EX.ROOT_CATEGORY_DIR);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            rdoLabel.Checked = true;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            for (int loop = ContentControls.Count - 1; loop >= 0; loop--)
            {                
                var key = ContentControls.Keys.ElementAt(loop);
                IDisposable dispose = ContentControls[key] as IDisposable;
                ContentControls.Remove(key);
                dispose?.Dispose();
            }
        }

        private void rdoLabel_CheckedChanged(object sender, EventArgs e)
        {
            if (!ContentControls.ContainsKey(rdoLabel))
            {
                ContentControls.Add(rdoLabel, new LabelViewsPanel());
            }

            Control ctrl = ContentControls[rdoLabel] as Control;
            if (!panel2.Controls.Contains(ctrl))
            {
                panel2.Controls.Add(ctrl);
                ctrl.Dock = DockStyle.Fill;
            }
            ctrl.BringToFront();
        }

        private void rdoControls_CheckedChanged(object sender, EventArgs e)
        {
            if (!ContentControls.ContainsKey(rdoControls))
            {
                ContentControls.Add(rdoControls, new ControlViewPanel());
            }

            Control ctrl = ContentControls[rdoControls] as Control;
            if (!panel2.Controls.Contains(ctrl))
            {
                panel2.Controls.Add(ctrl);
                ctrl.Dock = DockStyle.Fill;
            }
            ctrl.BringToFront();
        }

        private void btnRbnAlign_L_Click(object sender, EventArgs e)
        {
            bool hasException = false;
            PPT_COM_EX.PassTheCreatedSlide(out hasException, (app, slide) =>
            {
                try
                {
                    //선택된 shape들이 있을때만 정렬!!
                    if (app.ActiveWindow.Selection.Type != Microsoft.Office.Interop.PowerPoint.PpSelectionType.ppSelectionNone &&
                        app.ActiveWindow.Selection.Type == Microsoft.Office.Interop.PowerPoint.PpSelectionType.ppSelectionShapes )
                    {
                        app.ActiveWindow.Selection.ShapeRange.Align(Microsoft.Office.Core.MsoAlignCmd.msoAlignLefts, Microsoft.Office.Core.MsoTriState.msoFalse);
                    }
                }
                catch (Exception ex)
                {
                    hasException = true;
                    throw ex;
                }
            });
        }

        private void btnRbnAlign_C_Click(object sender, EventArgs e)
        {
            bool hasException = false;
            PPT_COM_EX.PassTheCreatedSlide(out hasException, (app, slide) =>
            {
                try
                {
                    //선택된 shape들이 있을때만 정렬!!
                    if (app.ActiveWindow.Selection.Type != Microsoft.Office.Interop.PowerPoint.PpSelectionType.ppSelectionNone &&
                        app.ActiveWindow.Selection.Type == Microsoft.Office.Interop.PowerPoint.PpSelectionType.ppSelectionShapes)
                    {
                        app.ActiveWindow.Selection.ShapeRange.Align(Microsoft.Office.Core.MsoAlignCmd.msoAlignCenters, Microsoft.Office.Core.MsoTriState.msoFalse);
                    }
                }
                catch (Exception ex)
                {
                    hasException = true;
                    throw ex;
                }
            });
        }

        private void btnRbnAlign_R_Click(object sender, EventArgs e)
        {
            bool hasException = false;
            PPT_COM_EX.PassTheCreatedSlide(out hasException, (app, slide) =>
            {
                try
                {
                    //선택된 shape들이 있을때만 정렬!!
                    if (app.ActiveWindow.Selection.Type != Microsoft.Office.Interop.PowerPoint.PpSelectionType.ppSelectionNone &&
                        app.ActiveWindow.Selection.Type == Microsoft.Office.Interop.PowerPoint.PpSelectionType.ppSelectionShapes)
                    {
                        app.ActiveWindow.Selection.ShapeRange.Align(Microsoft.Office.Core.MsoAlignCmd.msoAlignRights, Microsoft.Office.Core.MsoTriState.msoFalse);
                    }
                }
                catch (Exception ex)
                {
                    hasException = true;
                    throw ex;
                }
            });
        }

        private void btnRbnAlign_T_Click(object sender, EventArgs e)
        {
            bool hasException = false;
            PPT_COM_EX.PassTheCreatedSlide(out hasException, (app, slide) =>
            {
                try
                {
                    //선택된 shape들이 있을때만 정렬!!
                    if (app.ActiveWindow.Selection.Type != Microsoft.Office.Interop.PowerPoint.PpSelectionType.ppSelectionNone &&
                        app.ActiveWindow.Selection.Type == Microsoft.Office.Interop.PowerPoint.PpSelectionType.ppSelectionShapes)
                    {
                        app.ActiveWindow.Selection.ShapeRange.Align(Microsoft.Office.Core.MsoAlignCmd.msoAlignTops, Microsoft.Office.Core.MsoTriState.msoFalse);
                    }
                }
                catch (Exception ex)
                {
                    hasException = true;
                    throw ex;
                }
            });
        }

        private void btnRbnAlign_M_Click(object sender, EventArgs e)
        {
            bool hasException = false;
            PPT_COM_EX.PassTheCreatedSlide(out hasException, (app, slide) =>
            {
                try
                {
                    //선택된 shape들이 있을때만 정렬!!
                    if (app.ActiveWindow.Selection.Type != Microsoft.Office.Interop.PowerPoint.PpSelectionType.ppSelectionNone &&
                        app.ActiveWindow.Selection.Type == Microsoft.Office.Interop.PowerPoint.PpSelectionType.ppSelectionShapes)
                    {
                        app.ActiveWindow.Selection.ShapeRange.Align(Microsoft.Office.Core.MsoAlignCmd.msoAlignMiddles, Microsoft.Office.Core.MsoTriState.msoFalse);
                    }
                }
                catch (Exception ex)
                {
                    hasException = true;
                    throw ex;
                }
            });
        }

        private void btnRbnAlign_B_Click(object sender, EventArgs e)
        {
            bool hasException = false;
            PPT_COM_EX.PassTheCreatedSlide(out hasException, (app, slide) =>
            {
                try
                {
                    //선택된 shape들이 있을때만 정렬!!
                    if (app.ActiveWindow.Selection.Type != Microsoft.Office.Interop.PowerPoint.PpSelectionType.ppSelectionNone &&
                        app.ActiveWindow.Selection.Type == Microsoft.Office.Interop.PowerPoint.PpSelectionType.ppSelectionShapes)
                    {
                        app.ActiveWindow.Selection.ShapeRange.Align(Microsoft.Office.Core.MsoAlignCmd.msoAlignBottoms, Microsoft.Office.Core.MsoTriState.msoFalse);
                    }
                }
                catch (Exception ex)
                {
                    hasException = true;
                    throw ex;
                }
            });
        }
    }
}
