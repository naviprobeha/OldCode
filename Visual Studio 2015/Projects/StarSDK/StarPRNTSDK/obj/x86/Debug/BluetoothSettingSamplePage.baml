   M S B A M L   `   `   `  ����F  BStarPRNTSDK, Version=5.4.1.0, Culture=neutral, PublicKeyToken=null)clr-namespace:StarPRNTSDKStarPRNTSDK  5      ,    &StarPRNTSDK.BluetoothSettingSamplePage   5      R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      Hmc;http://schemas.openxmlformats.org/markup-compatibility/2006    5      >d2http://schemas.microsoft.com/expression/blend/2008    5      )localclr-namespace:StarPRNTSDK    5      -   5          N� Title$   BluetoothSettingSamplePage��5	       � 	Resources 5      �� 5   
      KeySecurityTypeArray   �� Type.   (StarPRNTSDK.BluetoothSecurityTypeManager"  6,   .s�5           Type$ DISABLE=�61     5      $	 SSP=�61     5      $ PINCODE=�61   5      (   "StarPRNTSDK.ListBoxItemWithTextBox  5   
   DeviceNameListBoxItem  !   StarPRNTSDK.BaseListBoxItem   Title$ Device Name��5      (   "StarPRNTSDK.ListBoxItemWithControl   ControlDisabledMessage$	 N/A��5      (   !TextBoxPreferredImeConversionMode$ Alphanumeric=�5         ForeGroundColor
 �   �   BorderBlushColor
 ����� 	   ListBoxItemTouchedCommand	 5      Z�   5   
   iOSPortNameListBoxItem  $ iOS Port Name��5      $	 N/A��5      $ Alphanumeric=�5      
 �   �
 �����	 5"      Z� )   #StarPRNTSDK.ListBoxItemWithCheckBox  5$   
   AutoConnectListBoxItem  $ Auto Connect��5&      $	 N/A��5'      
   CheckBoxIsChecked
 .  
 �   �
 �����	 5+      Z� )   #StarPRNTSDK.ListBoxItemWithComboBox  5-   
   SecurityListBoxItem  $ Security��5/      $	 N/A��50        SecurityTypeArray   BluetoothSecurities# [ 51         SelectedIndex$ 0��52      
 �   �
 �����	 55      Z�   57   
   ChangePinCodeListBoxItem  $ Change PIN Code��59      $	 N/A��5:      
 .  
 �   �
 �����	 5>      Z�   5@   
   NewPinCodeListBoxItem  $ New PIN Code��5B      $	 N/A��5C      $ Alphanumeric=�5D      
 �   �
 �����	 5G      Z� �� 5I   
    DeviceNameListBoxItemsSource  "  67   .s�5J      ��  �� ResourceKey$ DeviceNameListBoxItem��6&   5K      �� 5M   
   !iOSPortNameListBoxItemsSource  "  68   .s�5N      �� $ iOSPortNameListBoxItem��6&   5O      �� 5Q   
   !AutoConnectListBoxItemsSource  "  68   .s�5R      �� $ AutoConnectListBoxItem��6&   5S      �� 5U   
   SecurityListBoxItemsSource  "  65   .s�5V      �� $ SecurityListBoxItem��6&   5W      �� 5Y   
   #ChangePinCodeListBoxItemsSource  "  6:   .s�5Z      �� $ ChangePinCodeListBoxItem��6&   5[      �� 5]   
    NewPinCodeListBoxItemsSource  "  67   .s�5^      �� $ NewPinCodeListBoxItem��6&   5_      .��5c      $   StarPRNTSDK.CustomScrollViewer  .��5d   
   �  � RowDefinitions 5e      �� 5f      $
��Auto �6    �� 5g      $��* �6    �� 5h      $
��Auto �6     � ColumnDefinitions 5k      �� 5l      $��* �6#   �� 5m      $
��Auto �6#   .+�5p      �� $��0��6   $��0��6!   $��2��61     PageTitleStyle#��[ 6�   ��6E   ��   ResourceManager �� Source# [  �� Path$% BluetoothSettingSamplePageTitle�6�   �� 5r      -   5        $��0��5r      $��1��6"   $��Right=�62     RefreshButtonTemplete#��[ 6N   $��0, 0, 20, 0q�6�   6�   �� 5t      $��1��6   $��0��6&   $��2��66    �� Orientation$ Vertical=�6J   .+�5v      ��    DeviceNameListBoxItemsSource#��[ 6    " ListBoxItemWithTextBoxTemplate#��[ 6V     ListBoxStyle#��[ 6�     ListBoxItemStyle#��[ 6�   ��6�   Z� 6�   �� 5x       !	 iOSPortNameListBoxItemsSource#��[	 6   #��[ 6W   #��[ 6�   #��[ 6�   ��6�   Z� 6�   �� 5z       !
 AutoConnectListBoxItemsSource#��[
 6    # ListBoxItemWithCheckBoxTemplate#��[ 6W   #��[ 6�   #��[ 6�   ��6�   Z� 6�   �� 5|        SecurityListBoxItemsSource#��[ 6    # ListBoxItemWithComboBoxTemplate#��[ 6T   #��[ 6�   #��[ 6�   ��6�   Z� 6�   �� 5~       # ChangePinCodeListBoxItemsSource#��[ 6   #��[ 6Y   #��[ 6�   #��[ 6�   ��6�   Z� 6�   �� 5�         NewPinCodeListBoxItemsSource#��[ 6   #��[ 6V   #��[ 6�   #��[ 6�   ��6�   Z� 6�   5�      �� 5�      -   5         ��Name$ ApplyButton��5�      $��2��6*   $��1��67   $��Apply��6G     CallFunctionButtonStyle#��[ 6W   $��0, 0, 20, 20q�6�   6�   5�      5�      5�      