using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

using System.Resources;
using System.Reflection;

class PaFileExplorerDlg2016Mar25CS20 : Form{
	private ListView listview = new ListView();
	private TreeView treeview = new TreeView();
	private ImageList imagelist = new ImageList();
	private ResourceManager rm = new ResourceManager("PaFileExplorerDlg2016Mar25CS20",
		Assembly.GetExecutingAssembly());

	protected override Size DefaultSize{
		get{
			return new Size(800, 500);
		}
	}

	public PaFileExplorerDlg2016Mar25CS20(){
		this.Text = "PaFileExplorerDlg2016Mar25CS20";
		//this.ClientSize = new Size(800, 500);
		this.StartPosition = FormStartPosition.CenterScreen;
		this.MinimumSize = new Size(800, 500);

		treeview.Location = new Point(10, 10);
		listview.Location = new Point(245, 10);

		PA_DoWindowsResize();
		PA_DoWindowsInit();

		Controls.Add(treeview);
		Controls.Add(listview);

		this.Resize += new EventHandler(PaFileExplorerDlg2016Mar25CS20_Resize);
		treeview.AfterSelect += new TreeViewEventHandler(treeview_AfterSelect);
	}

	public void treeview_AfterSelect(object sender, TreeViewEventArgs e){
		//if(e.Action == TreeViewAction.ByMouse){
			//e.Node.Nodes.Clear();
			listview.Clear();
			listview.Columns.Add("文件名", 128, HorizontalAlignment.Left);
			listview.Columns.Add("文件大小(KB)", 128, HorizontalAlignment.Right);
			PA_DoCacheFolder(e.Node);
		//}
	}

	public void PA_DoCacheFolder(TreeNode node){
		string strFolder = node.FullPath;
		if(Directory.Exists(strFolder)){
			try{
				DirectoryInfo di = new DirectoryInfo(strFolder);
				DirectoryInfo[] diArr = di.GetDirectories();
				foreach(DirectoryInfo dii in diArr){
					TreeNodeCollection tnc = node.Nodes;
					bool isFound = false;
					for(int i=0; i<tnc.Count; i++){
						if(tnc[i].Text.CompareTo(dii.Name)==0){
							isFound = true;
							break;
						}
					}
					if(isFound==false){
						TreeNode tn = new TreeNode();
						tn.Text = dii.Name;
						tn.ImageIndex = 3;
						tn.SelectedImageIndex = 4;
						node.Nodes.Add(tn);
					}
				}
				node.Expand();
				int n = 0;
				FileInfo[] fiArr = di.GetFiles();
				foreach(FileInfo fii in fiArr){
					ListViewItem lvi = new ListViewItem(fii.Name, n++);
					lvi.ImageIndex = 0;
					long nFileLength = fii.Length/1024;
					if((fii.Length % 1024)>0) nFileLength++;
					lvi.SubItems.Add(nFileLength.ToString() + " KB");
					listview.Items.Add(lvi);
				}

			}catch(Exception ex){
				MessageBox.Show(ex.Message);
			}
		}
	}

	public void PaFileExplorerDlg2016Mar25CS20_Resize(object sender, EventArgs e){
		PA_DoWindowsResize();
	}

	public void PA_DoWindowsResize(){
		int nClientWidth1 = ClientRectangle.Width - (230 + 25);
		int nClientHeight1 = ClientRectangle.Height - 20;
		treeview.Size = new Size(230, nClientHeight1);
		listview.Size = new Size(nClientWidth1, nClientHeight1);
	}

	public void PA_DoWindowsInit(){

		this.Icon = (Icon)(rm.GetObject("this.ico"));

		imagelist.Images.Add((Image)(rm.GetObject("icon-file.png")));
		imagelist.Images.Add((Image)(rm.GetObject("icon-hdd.png")));
		imagelist.Images.Add((Image)(rm.GetObject("icon-cdrom.png")));
		imagelist.Images.Add((Image)(rm.GetObject("icon-folder.png")));
		imagelist.Images.Add((Image)(rm.GetObject("icon-folder-open.png")));

		treeview.BackColor = Color.White;
		treeview.PathSeparator = "\\";
		treeview.ImageList = imagelist;

		listview.View = View.Details;
		listview.GridLines = true;
		listview.FullRowSelect = true;
		listview.SmallImageList = imagelist;

		DriveInfo[] drArr = DriveInfo.GetDrives();
		foreach (DriveInfo dri in drArr)
		{
			TreeNode tn = new TreeNode();
			tn.Text = dri.Name;
			int nImageIndex = dri.DriveType==DriveType.CDRom ? 2 : 1;
			tn.ImageIndex = nImageIndex;
			tn.SelectedImageIndex = nImageIndex;
			treeview.Nodes.Add(tn);
			//PA_DoCacheFolder(tn);
		}
		 
	}

	public static void Main(string[] args){
		Application.Run(new PaFileExplorerDlg2016Mar25CS20());
	}
}