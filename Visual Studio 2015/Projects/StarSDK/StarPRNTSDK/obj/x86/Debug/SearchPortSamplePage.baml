   M S B A M L   `   `   `  ����F  BStarPRNTSDK, Version=5.4.1.0, Culture=neutral, PublicKeyToken=null)clr-namespace:StarPRNTSDKStarPRNTSDK  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      A =StarIO, Version=2.3.1.0, Culture=neutral, PublicKeyToken=nullK2clr-namespace:StarMicronics.StarIO;assembly=StarIOStarMicronics.StarIO 5      &     StarPRNTSDK.SearchPortSamplePage   5      R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      Hmc;http://schemas.openxmlformats.org/markup-compatibility/2006    5      >d2http://schemas.microsoft.com/expression/blend/2008    5      )localclr-namespace:StarPRNTSDK    5      4sys&clr-namespace:System;assembly=mscorlib    5      CstarIO2clr-namespace:StarMicronics.StarIO;assembly=StarIO    5      -   5          N� Title$  SearchPortSamplePage��5       � 	Resources 5      �� 5   
      KeyInterFaceTypeArray   �� Type&    StarPRNTSDK.InterfaceInformation"  6-   .s�5           Type$ USBVendorClass=�6)     5      $ USBPrinterClass=�6)     5      $ Ethernet=�6)     5      $ 	Bluetooth=�6)     5      $ Serial=�6)     5      $ Parallel=�6)     5         ManualSetting$	 All=�6)     5      $ Manual=�6)   5      �� 5   
   PrinterModelArray  "  StarPRNTSDK.ModelInformation"  6,   .s�5           Model$ POP10=�6%     5      $ FVP10=�6%     5      $ TSP113=�6%     5      $ TSP654II=�6%     5      $ TSP743II=�6%     5      $ TSP847II=�6%     5       $ SP747=�6%     5!      $ S210i=�6%     5"      $ S220i=�6%     5#      $ S230i=�6%     5$      $ T300i=�6%     5%      $ T400i=�6%     5&      $
 L200=�6%     5'      $
 L300=�6%     5(      $ BSC10=�6%     5)      $ S210i_StarPRNT=�6%     5*      $ S220i_StarPRNT=�6%     5+      $ S230i_StarPRNT=�6%     5,      $ T300i_StarPRNT=�6%     5-      $ T400i_StarPRNT=�6%   5.      �� 50   
   ManualSettingsArray  " ��6.   .s�51      ��	Port Name��6$   ��52      Port Settings��6(   53      �� 55   
   ($ManualPortSettingsForSerialPortArray  " ��6?   .s�56      ��Port Settings��6(   57      �� 59   
   DrawerOpenStatusArray  " ��60   .s�5:      ��High when Open��6)   ��5;      Low when Open��6(   5<      %   StarPRNTSDK.SelectSettingWindow  5>   
   SelectInterfaceWindow     SettingTitle$ Select Interface.��6B     InterFaceTypeArray   InterfaceInformations# [ 6c      SelectedIndex$ 0��6�     5@   
   SelectModelWindow  	 � Title$	 Confirm��6>   $ What is your printer?��6N     PrinterModelArray
   ModelInformations#
 [ 6s   $ 0��6�   !   StarPRNTSDK.EnterTextWindow  5B   
   ManualSettingWindow     SettingTitle$3 -Please enter the port name and port settings.��6<     ManualSettingsArray   Settings# [ 6y     5D   
   )%ManualPortSettingsForSerialPortWindow  $2 ,Please enter the your printer port settings.��6N    ( $ManualPortSettingsForSerialPortArray# [ 6�     5F   
    SelectDrawerOpenStatusWindow  $$ Select CashDrawer Open Status.��6I     DrawerOpenStatusArray   Settings# [ 6w   $ 0��6�   �� 5H   
   PortListBoxItemTemplate   �� DataType!   StarPRNTSDK.PortInfoManager"  67   .R�5I      ��  �� Orientation$ Vertical=�6   .+�5J      �� -   5        
�������  ListBoxItemButtonTemplete#��[ 5J   E   
 � Tag 6   �� *
PortInfo+��5K      �� 5L      $ Vertical=�6%   .+�5M      �� $��20(�6C   
����  �$��10, 0, 0, 0q�6c   ��6(   �� *	ModelName+6c   �� 5N      $��15(�6E   
����  �$��10, 0, 0, 0q�6e   ��6(   �� *Description+6e   5O      5Q      �� 5S      $��Center=�6   	 �� X1$ 0��5T   '   	 �� Y1$ 0��5U   '   	 �� Y2$ 0��6.   
�������$��2��5V   :   	 �� X2 5T   .   ��  �� Path$ ActualWidth� �� RelativeSource ��  �� Mode$
 Self=�5V   :   5W      5X      .��5\      $   StarPRNTSDK.CustomScrollViewer  .��5]   
   �  � RowDefinitions 5^      �� 5_      $
��Auto �6    �� 5`      $
��Auto �6    �� 5a      $��* �6    .+�5d      �� $��0��6     PageTitleStyle#��[ 6}   ��6!   ��   ResourceManager �� Source# [ $ SearchPortSamplePageTitle�6}   �� 5f      -   5        $��0��5f      $��Right=�6#    	 RefreshButtonTemplete#��[	 6?   $��0, 0, 20, 0q�6q   6�   �� 5h      $��1��6   $
��List��6!    
 ListBoxSeparatorStyle#��[
 60   �� 5j      -   5         ��Name$ PortListBox��5j      $��2��6+     PortListBoxItemTemplate#��[ 68     ListBoxStyle#��[ 6p     ListBoxItemStyle#��[ 6�   ��6�   Z� 5k      5l      5n      