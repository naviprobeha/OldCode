   M S B A M L   `   `   `  ����F  BStarPRNTSDK, Version=5.4.1.0, Culture=neutral, PublicKeyToken=null)clr-namespace:StarPRNTSDKStarPRNTSDK  5      4    .StarPRNTSDK.PrinterDriverWithDisplaySamplePage   5      R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      Hmc;http://schemas.openxmlformats.org/markup-compatibility/2006    5      >d2http://schemas.microsoft.com/expression/blend/2008    5      )localclr-namespace:StarPRNTSDK    5      -   5          N� Title$(  "PrinterDriverWithDisplaySamplePage��5	       � 	Resources 5      (   "StarPRNTSDK.DisplayFunctionManager   DisplayFunctionManagerExt��%   &	          5   
   6'   5      .��5      $   StarPRNTSDK.CustomScrollViewer  .��5   
   �  � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $��* �6    �� 5      $
��Auto �6    .+�5      �� $��0��6     PageTitleStyle#��[ 6�   ��6!   ��   ResourceManager �� Source# [  �� Path$- 'PrinterDriverWithDisplaySamplePageTitle�6�   �� 5      -   5        $��0��5      $��Right=�6"     RefreshButtonTemplete#��[ 6>   $��0, 0, 20, 0q�6p   6�   �� 5      -   5         ��Name$ JobCountTextBlock��5      $��1��63     BaseTextBlockStyle#��[ 6@   �� 5!      $��2��6   $��Contents��6!     ListBoxSeparatorStyle#��[ 64   �� 5#      $��3��6     ListBoxItemTemplate#��[ 6#     ListBoxStyle#��[ 6n     ListBoxItemStyle#��[ 6�   ��6W   Z� .@�5$      !   StarPRNTSDK.BaseListBoxItem     ForeGroundColor
 ��  �   BorderBlushColor
 ����� 	 DisplayTextExtCommand    ListBoxItemTouchedCommand# [	 5(      	   Title	 5%      ��  
 TextFunction# [
 $ Title�5(        5*      
 ��  �
 �����  DisplayGraphicExtCommand# [ 5.      	 5+      ��   GraphicFunction# [ $ Title�5.        50      
 ��  �
 �����  DisplayTurnOnOffExtCommand# [ 54      	 51      ��   TurnOnOffFunction# [ $ Title�54        56      
 ��  �
 �����  DisplayCursorExtCommand# [ 5:      	 57      ��   CursorFunction# [ $ Title�5:        5<      
 ��  �
 �����  DisplayContrastExtCommand# [ 5@      	 5=      ��   ContrastFunction# [ $ Title�5@        5B      $	 Character Set��5C      
 ��  �
 ����� ! DisplayCharacterSetExtCommand# [ 5F        5H      
 ��  � ) %DisplayUserDefinedCharacterExtCommand# [ 5K      	 5I      ��    UserDefinedCharacterFunction# [ $ Title�5K      5M      �� 5O      -   5        $ StatusTextBlock��5O      $��4��61     ExtStatusTextBlockStyle#��[ 6>   � 5Q      $��5��6   
 � ColumnDefinitions
 5S      �� 5T      $��* �6'   �� 5U      $
��Auto �6'   �� 5V      $��* �6'   �� 5W      $
��Auto �6'   �� 5X      $��* �6'   .+�5[      �� -   5         ��Name$ SelectPatternComboBox��5[   �   $��1��6   $��Center=�5]        ComboBoxStyle#��[ 5^      ��5[   +   �� # [  $ GridColumnSpan� �� UpdateSourceTrigger$ PropertyChanged=���5\      �� # [  $ Patterns�$ PropertyChanged=� �� NotifyOnTargetUpdated . 5`      �� 5b      -   5        $# SelectAdditionPatternComboBox��5b   +   $��3��6   $��Center=�5d      #��[ 5e      ��5c      �� # [  $ AdditionPatterns�$ PropertyChanged=�y�5f      �� # [  $ AdditionPatternVisibility�$ PropertyChanged=�5g      5i      �� 5k      -   5         ��Name$ StatusMonitorCheckBox��5k      $��6��66   $
��Left=�6C   $��Status Monitor��6^     CheckBoxStyle#��[ 6w   $��20, 0, 0, 20q�6�    d� 	IsChecked 6�   �� *IsMonitoring+ �� Mode$ TwoWay=�6  �� 5m      -   5        $��6��5m      $��Print via Printer Driver��6"   $��Right=�6E   $	��400��6a     CallFunctionButtonStyle#��[ 6m   $��0, 0, 20, 20q�6�   6�   5o      5q      5r      