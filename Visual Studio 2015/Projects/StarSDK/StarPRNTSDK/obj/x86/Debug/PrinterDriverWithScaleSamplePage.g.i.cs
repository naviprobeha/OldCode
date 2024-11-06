﻿#pragma checksum "..\..\..\PrinterDriverWithScaleSamplePage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "701C5761AB0C82DBB9E161CA90F8700C208AF3EC"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using StarPRNTSDK;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace StarPRNTSDK {
    
    
    /// <summary>
    /// PrinterDriverWithScaleSamplePage
    /// </summary>
    public partial class PrinterDriverWithScaleSamplePage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\PrinterDriverWithScaleSamplePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock JobCountTextBlock;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\PrinterDriverWithScaleSamplePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock StatusTextBlock;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\PrinterDriverWithScaleSamplePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox StatusMonitorCheckBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/StarPRNTSDK;component/printerdriverwithscalesamplepage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\PrinterDriverWithScaleSamplePage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\..\PrinterDriverWithScaleSamplePage.xaml"
            ((StarPRNTSDK.PrinterDriverWithScaleSamplePage)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\PrinterDriverWithScaleSamplePage.xaml"
            ((StarPRNTSDK.PrinterDriverWithScaleSamplePage)(target)).Unloaded += new System.Windows.RoutedEventHandler(this.Page_Unloaded);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 23 "..\..\..\PrinterDriverWithScaleSamplePage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.RefreshButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.JobCountTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.StatusTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            
            #line 29 "..\..\..\PrinterDriverWithScaleSamplePage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ZeroClearButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 31 "..\..\..\PrinterDriverWithScaleSamplePage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.UnitChangeButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.StatusMonitorCheckBox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 33 "..\..\..\PrinterDriverWithScaleSamplePage.xaml"
            this.StatusMonitorCheckBox.Checked += new System.Windows.RoutedEventHandler(this.StatusMonitorCheckBox_CheckedChanged);
            
            #line default
            #line hidden
            
            #line 33 "..\..\..\PrinterDriverWithScaleSamplePage.xaml"
            this.StatusMonitorCheckBox.Unchecked += new System.Windows.RoutedEventHandler(this.StatusMonitorCheckBox_CheckedChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 35 "..\..\..\PrinterDriverWithScaleSamplePage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PrintViaPrinterDriverButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

