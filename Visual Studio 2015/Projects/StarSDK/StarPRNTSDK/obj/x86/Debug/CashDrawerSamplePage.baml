   M S B A M L   `   `   `  ����F  BStarPRNTSDK, Version=5.4.1.0, Culture=neutral, PublicKeyToken=null)clr-namespace:StarPRNTSDKStarPRNTSDK  5      &     StarPRNTSDK.CashDrawerSamplePage   5      R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      Hmc;http://schemas.openxmlformats.org/markup-compatibility/2006    5      >d2http://schemas.microsoft.com/expression/blend/2008    5      )localclr-namespace:StarPRNTSDK    5        N� Title$  CashDrawerSamplePage��5	       � 	Resources 5      *   $StarPRNTSDK.OpenCashDrawerClickEvent !  Channel1CheckConditionCommand   Channel   CheckCondition   StarPRNTSDK.BaseCommand   
Executable & "Channel1DoNotCheckConditionCommand ! Channel2CheckConditionCommand & "Channel2DoNotCheckConditionCommand:   4StarPRNTSDK.GoNextPageWithSetCashDrawerOpenParameter 4 0GoCashDrawerExtPageChannel1CheckConditionCommand   Channel   CheckCondition#   StarPRNTSDK.GoNextPageCommand   NavigationUri 9 5GoCashDrawerExtPageChannel1DoNotCheckConditionCommand 4 0GoCashDrawerExtPageChannel2CheckConditionCommand 9 5GoCashDrawerExtPageChannel2DoNotCheckConditionCommand��%p  &	        &	 0     &	 `     &	 �     &	 �     &	     &	 p    &	 �      5   
   $	 No1=�6O    .  . 6s     5   
   $	 No1=�6T    .   . 6y     5   
   $	 No2=�6O    .  . 6s     5   
   $	 No2=�6T    .   . 6y     5   
   $	 No1=�6r    . $" CashDrawerExtSamplePage.xaml?�6�    . 6�     5   
   $	 No1=�6w    .  $" CashDrawerExtSamplePage.xaml?�6�    . 6�     5   
   $	 No2=�6r    . $" CashDrawerExtSamplePage.xaml?�6�    . 6�     5   
   $	 No2=�6w    .  $" CashDrawerExtSamplePage.xaml?�6�    . 6�   5      .��5      $   StarPRNTSDK.CustomScrollViewer  .��5   
   ��  �� Orientation$ Vertical=�6   .+�5      ��   PageTitleStyle#��[ 6p   ��6   ��  	 ResourceManager	 �� Source#	 [	 
 �� Path$
 CashDrawerSamplePageTitle�6p   �� 5       
 ListBoxSeparatorStyle#��[
 6k   ��6   �� #	 [	 $
 StarIOSDKSampleTitle�6k   �� 5        ListBoxItemTemplate#��[ 6     ListBoxStyle#��[ 6a     ListBoxItemStyle#��[ 6�   ��6J   Z� .@�5      !   StarPRNTSDK.BaseListBoxItem     Title$! Channel1 (Check condition.)��5         ForeGroundColor
 ��  �   BorderBlushColor
 �����    ListBoxItemTouchedCommand# [  5"        5$      $( "Channel1 (Do not check condition.)��5%      
 ��  �
 �����# [ 5(        5*      $! Channel2 (Check condition.)��5+      
 ��  �
 �����# [ 5.        50      $( "Channel2 (Do not check condition.)��51      
 ��  �# [ 53      55      �� 57      #��[
 6r   ��6   �� #	 [	 $!
 StarIOExtManagerSampleTitle�6r   �� 59      #��[ 6   #��[ 6a   #��[ 6�   ��6J   Z� .@�5:        $! Channel1 (Check condition.)��5;      
 ��  �
 �����# [ 5>        5@      $( "Channel1 (Do not check condition.)��5A      
 ��  �
 �����# [ 5D        5F      $! Channel2 (Check condition.)��5G      
 ��  �
 �����# [ 5J        5L      $( "Channel2 (Do not check condition.)��5M      
 ��  �
 �����# [ 5P      5R      5T      5U      5V      