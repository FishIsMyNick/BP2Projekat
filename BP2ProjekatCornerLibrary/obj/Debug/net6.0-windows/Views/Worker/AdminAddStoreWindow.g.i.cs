﻿#pragma checksum "..\..\..\..\..\Views\Worker\AdminAddStoreWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "35256E58D23D1BB35DB2FD2242A4656494D723A8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using BP2ProjekatCornerLibrary.Views.Worker;
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


namespace BP2ProjekatCornerLibrary.Views.Worker {
    
    
    /// <summary>
    /// AdminAddStoreWindow
    /// </summary>
    public partial class AdminAddStoreWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 56 "..\..\..\..\..\Views\Worker\AdminAddStoreWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Naziv;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\..\Views\Worker\AdminAddStoreWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Ulica;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\..\..\Views\Worker\AdminAddStoreWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Broj;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\..\..\Views\Worker\AdminAddStoreWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Mesto;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\..\..\Views\Worker\AdminAddStoreWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Drzava;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\..\..\Views\Worker\AdminAddStoreWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Cancel;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\..\..\..\Views\Worker\AdminAddStoreWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Confirm;
        
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
            System.Uri resourceLocater = new System.Uri("/BP2ProjekatCornerLibrary;V1.0.0.0;component/views/worker/adminaddstorewindow.xam" +
                    "l", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\Worker\AdminAddStoreWindow.xaml"
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
            this.Naziv = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.Ulica = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.Broj = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.Mesto = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.Drzava = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.btn_Cancel = ((System.Windows.Controls.Button)(target));
            
            #line 97 "..\..\..\..\..\Views\Worker\AdminAddStoreWindow.xaml"
            this.btn_Cancel.Click += new System.Windows.RoutedEventHandler(this.btn_Cancel_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btn_Confirm = ((System.Windows.Controls.Button)(target));
            
            #line 102 "..\..\..\..\..\Views\Worker\AdminAddStoreWindow.xaml"
            this.btn_Confirm.Click += new System.Windows.RoutedEventHandler(this.btn_Confirm_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

