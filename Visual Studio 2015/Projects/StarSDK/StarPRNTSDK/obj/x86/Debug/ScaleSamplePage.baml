   M S B A M L   `   `   `  ����F  BStarPRNTSDK, Version=5.4.1.0, Culture=neutral, PublicKeyToken=null)clr-namespace:StarPRNTSDKStarPRNTSDK  5      !    StarPRNTSDK.ScaleSamplePage   5      R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      Hmc;http://schemas.openxmlformats.org/markup-compatibility/2006    5      >d2http://schemas.microsoft.com/expression/blend/2008    5      )localclr-namespace:StarPRNTSDK    5        N� Title$  ScaleSamplePage��5	       � 	Resources 5      -   'StarPRNTSDK.CallScaleFunctionClickEvent   CheckStatusCommand   Type   StarPRNTSDK.BaseCommand   
Executable  DisplayedWeightCommand  ZeroClearCommand  UnitChangeCommand#   StarPRNTSDK.GoNextPageCommand  GoScaleExtSamplePageCommand   NavigationUri��%4  &	        &	 1     &	 f     &	 �     &	 �       5   
   $ CheckStatus=�6G    . 6Z     5   
   $ DisplayedWeight=�6K    . 6b     5   
   $ 	ZeroClear=�6E    . 6V     5   
   $ 
UnitChange=�6F    . 6X     5   
   $ ScaleExtSamplePage.xaml?�6F    . 6n   5      .��5      $   StarPRNTSDK.CustomScrollViewer  .��5   
   ��  �� Orientation$ Vertical=�6   .+�5      ��   PageTitleStyle#��[ 6k   ��6   ��   ResourceManager �� Source# [  �� Path$ ScaleSamplePageTitle�6k   �� 5      $��Contents��6     ListBoxSeparatorStyle#��[ 6'   �� 5        ListBoxItemTemplate#��[ 6    	 ListBoxStyle#��[	 6a    
 ListBoxItemStyle#��[
 6�   ��6J   Z� .@�5      !   StarPRNTSDK.BaseListBoxItem     Title$ Check Status��5      	   ForeGroundColor
	 ��  �
   BorderBlushColor

 �����    ListBoxItemTouchedCommand# [  5        5!      $ Displayed Weight��5"      
	 ��  �

 �����# [ 5%        5'      $ 
Zero Clear��5(      
	 ��  �

 �����# [ 5+        5-      $ Unit Change��5.      
	 ��  �# [ 50      53      �� 55      #��[ 6r   ��6   �� # [ $! StarIOExtManagerSampleTitle�6r   �� 57      #��[ 6   #��[	 6a   #��[
 6�   ��6J   Z� .@�58        $ Sample��59      
	 ��  �

 �����# [ 5<      5?      5A      5B      5C      