﻿#pragma checksum "..\..\..\..\..\..\Views\Worker\Admin\AdminEditFormatWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "661DC8F96DDA0F06527C8898987D382BA1D36BDD"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using BP2ProjekatCornerLibrary.Views.Worker.Admin;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace BP2ProjekatCornerLibrary.Views.Worker.Admin {
    
    
    /// <summary>
    /// AdminEditFormatWindow
    /// </summary>
    public partial class AdminEditFormatWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 34 "..\..\..\..\..\..\Views\Worker\Admin\AdminEditFormatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Format_Sort;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\..\..\Views\Worker\Admin\AdminEditFormatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image img_Period_Sort;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\..\..\Views\Worker\Admin\AdminEditFormatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView Formati;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\..\..\..\Views\Worker\Admin\AdminEditFormatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_Edit_Format;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\..\..\..\..\Views\Worker\Admin\AdminEditFormatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Edit_Confirm;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\..\..\..\Views\Worker\Admin\AdminEditFormatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Edit_Delete;
        
        #line default
        #line hidden
        
        
        #line 116 "..\..\..\..\..\..\Views\Worker\Admin\AdminEditFormatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Edit_Cancel;
        
        #line default
        #line hidden
        
        
        #line 139 "..\..\..\..\..\..\Views\Worker\Admin\AdminEditFormatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_Add_Format;
        
        #line default
        #line hidden
        
        
        #line 155 "..\..\..\..\..\..\Views\Worker\Admin\AdminEditFormatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Add_Confirm;
        
        #line default
        #line hidden
        
        
        #line 160 "..\..\..\..\..\..\Views\Worker\Admin\AdminEditFormatWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Add_Cancel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/BP2ProjekatCornerLibrary;V1.0.0.0;component/views/worker/admin/admineditformatwi" +
                    "ndow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\Views\Worker\Admin\AdminEditFormatWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.btn_Format_Sort = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\..\..\..\..\Views\Worker\Admin\AdminEditFormatWindow.xaml"
            this.btn_Format_Sort.Click += new System.Windows.RoutedEventHandler(this.btn_Format_Sort_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.img_Period_Sort = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.Formati = ((System.Windows.Controls.ListView)(target));
            
            #line 51 "..\..\..\..\..\..\Views\Worker\Admin\AdminEditFormatWindow.xaml"
            this.Formati.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Formati_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.tb_Edit_Format = ((System.Windows.Controls.TextBox)(target));
            
            #line 93 "..\..\..\..\..\..\Views\Worker\Admin\AdminEditFormatWindow.xaml"
            this.tb_Edit_Format.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.tb_Edit_Format_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btn_Edit_Confirm = ((System.Windows.Controls.Button)(target));
            
            #line 110 "..\..\..\..\..\..\Views\Worker\Admin\AdminEditFormatWindow.xaml"
            this.btn_Edit_Confirm.Click += new System.Windows.RoutedEventHandler(this.btn_Edit_Confirm_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btn_Edit_Delete = ((System.Windows.Controls.Button)(target));
            
            #line 115 "..\..\..\..\..\..\Views\Worker\Admin\AdminEditFormatWindow.xaml"
            this.btn_Edit_Delete.Click += new System.Windows.RoutedEventHandler(this.btn_Edit_Delete_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btn_Edit_Cancel = ((System.Windows.Controls.Button)(target));
            
            #line 119 "..\..\..\..\..\..\Views\Worker\Admin\AdminEditFormatWindow.xaml"
            this.btn_Edit_Cancel.Click += new System.Windows.RoutedEventHandler(this.btn_Edit_Cancel_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.tb_Add_Format = ((System.Windows.Controls.TextBox)(target));
            
            #line 143 "..\..\..\..\..\..\Views\Worker\Admin\AdminEditFormatWindow.xaml"
            this.tb_Add_Format.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.tb_Add_Format_TextChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btn_Add_Confirm = ((System.Windows.Controls.Button)(target));
            
            #line 159 "..\..\..\..\..\..\Views\Worker\Admin\AdminEditFormatWindow.xaml"
            this.btn_Add_Confirm.Click += new System.Windows.RoutedEventHandler(this.btn_Add_Confirm_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btn_Add_Cancel = ((System.Windows.Controls.Button)(target));
            
            #line 163 "..\..\..\..\..\..\Views\Worker\Admin\AdminEditFormatWindow.xaml"
            this.btn_Add_Cancel.Click += new System.Windows.RoutedEventHandler(this.btn_Add_Cancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

