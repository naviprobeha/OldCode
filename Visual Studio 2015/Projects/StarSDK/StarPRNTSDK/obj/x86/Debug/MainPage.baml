   M S B A M L   `   `   `  ����F  BStarPRNTSDK, Version=5.4.1.0, Culture=neutral, PublicKeyToken=null)clr-namespace:StarPRNTSDKStarPRNTSDK  5          StarPRNTSDK.MainPage   5      R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      Hmc;http://schemas.openxmlformats.org/markup-compatibility/2006    5      >d2http://schemas.microsoft.com/expression/blend/2008    5      )localclr-namespace:StarPRNTSDK    5      -   5          N� Title$  MainPage��5	       � 	Resources 5      �� 5   
      KeyAllLanguageArray   �� Type   StarPRNTSDK.Language"  6+   .s�5           Type$ English=�6     5      $ Japanese=�6     5      $ French=�6     5      $ 
Portuguese=�6     5      $ Spanish=�6     5      $ German=�6     5      $ Russian=�6     5      $ SimplifiedChinese=�6     5      $ TraditionalChinese=�6   5      �� 5   
   	EnJpArray  "  6$   .s�5        $ English=�6     5      $ Japanese=�6   5      �� 5   
   ForAllReceiptsLanguageArray  "  66   .s�5        $ English=�6     5      $ Japanese=�6     5       $ French=�6     5!      $ 
Portuguese=�6     5"      $ Spanish=�6     5#      $ German=�6   5$      �� 5&   
   AllPaperSizeArray     StarPRNTSDK.PaperSize"  6,   .s�5'           Type$ TwoInch=�6     5(      $ 	ThreeInch=�6     5)      $ FourInch=�6   5*      %   StarPRNTSDK.SelectSettingWindow  5,   
   SelectAllLanguageWindow     SettingTitle$ Select language.��6D     AllLanguageArray   	Languages# [ 6d      SelectedIndex$ 0��6�     5-   
   SelectEnJpWindow  $ Select language.��6=     	EnJpArray# [ 6]   $ 0��6�     5.   
   &"SelectForAllReceiptsLanguageWindow  $ Select language.��6O     ForAllReceiptsLanguageArray# [ 6o   $ 0��6�     50   
   SelectAllPaperSizeWindow  $ Select paper size.��6E     AllPaperSizeArray   PaperSizeCollection# [ 6g   $ 0��6�   #   StarPRNTSDK.GoNextPageCommand  52   
   GoSearchPortPageCommand  	   NavigationUri$	 SearchPortSamplePage.xaml?�6B      StarPRNTSDK.BaseCommand
   
Executable
 . 6l   4   .StarPRNTSDK.GoNextPageCommandWithSelectSetting  54   
   GoPrinterSamplePageCommand  $	 PrinterSamplePage.xaml?�6V     SelectAllLanguageWindow   SelectLanguageWindow# [ 6}     SelectAllPaperSizeWindow   SelectPaperSizeWindow# [ 6�   $   IsStaticPaperSizeForEmulation . 
 6$  ��   SelectedModelManager �� Source# [  �� Path$ PrinterSampleEnabled� �� Mode$ OneWay=�  56   
    GoBlackMarkSamplePageCommand  $	 BlackMarkSamplePage.xaml?�6X     SelectEnJpWindow# [ 6�   
 6�   �� # [ $ BlackMarkSampleEnabled�$ OneWay=�  58   
   %!GoBlackMarkPasteSamplePageCommand  $#	 BlackMarkPasteSamplePage.xaml?�6]   # [ 6�   
 6�   �� # [ $ BlackMarkSampleEnabled�$ OneWay=�  5:   
   GoPageModeSamplePageCommand  $	 PageModeSamplePage.xaml?�6W   # [ 6   # [ 6�   
 6�   �� # [ $ PageModeSampleEnabled�$ OneWay=�  5<   
   GoCashDrawerPageCommand  $	 CashDrawerSamplePage.xaml?�6B   
 6l   �� # [ $ CashDrawerSampleEnabled�$ OneWay=�  5>   
   '#GoBarcodeReaderExtSamplePageCommand  $%	 BarcodeReaderExtSamplePage.xaml?�6_      ModelConfirmMessage$ This menu is for mPOP��6�   
 6�   �� # [ $  BarcodeReaderSampleEnabled�$ OneWay=�  5@   
   GoDisplaySamplePageCommand  $	 DisplaySamplePage.xaml?�6V   $) #This menu is for mPOP or TSP100IIIU��6}   
 6�   �� # [ $ DisplaySampleEnabled�$ OneWay=�  5B   
   GoScaleSamplePageCommand  $	 ScaleSamplePage.xaml?�6T   $ This menu is for mPOP��6y   
 6�   �� # [ $ ScaleSampleEnabled�$ OneWay=�  5D   
   "GoCombinationSamplePageCommand  $ 	 CombinationSamplePage.xaml?�6Z   $ This menu is for mPOP��6�   # [ 6�   # [ 6�       IsStaticPaperSizeForModel . 
 6T  �� # [ $ CombinationSampleEnabled�$ OneWay=�  5F   
   /+GoCombinationPrinterDriverSamplePageCommand  $-	 'CombinationPrinterDriverSamplePage.xaml?�6g   # [ 6�   # [ 6�    . 
 6F  �� # [ $+ %CombinationPrinterDriverSampleEnabled�$ OneWay=�  5H   
   GoAPISamplePageCommand  $	 APISamplePage.xaml?�6R   # [ 6u    . 
 6�   �� # [ $ APISampleEnabled�$ OneWay=�  5J   
   "GoAllReceiptsSamplePageCommand  $ 	 AllReceiptsSamplePage.xaml?�6Z    &	 "SelectForAllReceiptsLanguageWindow# [	 6�   # [ 6�   
 6  �� # [ $ AllReceiptsSampleEnabled�$ OneWay=�  5L   
   #GoDeviceStatusSamplePageCommand  $!	 DeviceStatusSamplePage.xaml?�6J   
 6v   �� # [ $ DeviceStatusSampleEnabled�$ OneWay=�,   &StarPRNTSDK.ShowSerialNumberClickEvent  5N   
   ShowSerialNumberCommand  
 6K   �� # [ $ SerialNumberSampleEnabled�$ OneWay=�  5P   
   '#GoBluetoothSettingSamplePageCommand  $%	 BluetoothSettingSamplePage.xaml?�6N   
 6~   �� # [ $ BluetoothSettingEnabled�$ OneWay=�.   (StarPRNTSDK.ShowLibraryVersionClickEvent  5R   
   ShowLibraryVersionCommand  
 . 6O   .��5U      $	   StarPRNTSDK.CustomScrollViewer	  .��5V   
   ��  �� Orientation$ Vertical=�6   .+�5W      �� $ 
Horizontal=�6   $��Center=�62   .+�5X      ��  
 PageTitleStyle#��[
 6c   ��6   ��   ResourceManager# [ $ AppTitle�6c   �� 5Y      $
��Ver.��6   #��[
 6'   �� 5Z      #��[
 6b   ��6   �� # [ $ Version�6b   5[      �� 5]      $��Destination Device��6     ListBoxSeparatorStyle#��[ 61   �� 5_        ListBoxItemTemplate#��[ 6     ListBoxStyle#��[ 6a     ListBoxItemStyle#��[ 6�   ��6J   Z� .@�5`      !
   StarPRNTSDK.BaseListBoxItem
   
  ForeGroundColor
 ��  �  GoSearchPortPageCommand  
  ListBoxItemTouchedCommand# [ 5d       
  Title 5a      �� # [ $ SelectedModelDescription� 
  IsAnimation 5c      �� # [ $ 
IsUnSelect�5d      5e      �� 5g      $��Printer��6   #��[ 6&   �� 5i      #��[ 6   #��[ 6a   #��[ 6�   ��6J   Z� .@�5j      
  $ Sample��5k      
 ��  � 
  BorderBlushColor
 �����  GoPrinterSamplePageCommand# [ 5n      
  5p      $ Black Mark Sample��5q      
 ��  �
 �����   GoBlackMarkSamplePageCommand# [ 5t      
  5v      $ Black Mark Sample (Paste)��5w      
 ��  �
 ����� % !GoBlackMarkPasteSamplePageCommand# [ 5z      
  5|      $ Page Mode Sample��5}      
 ��  �  GoPageModeSamplePageCommand# [ 5      5�      �� 5�      $��Cash Drawer��6   #��[ 6*   �� 5�      #��[ 6   #��[ 6a   #��[ 6�   ��6J   Z� .@�5�      
  $ Sample��5�      
 ��  �  GoCashDrawerPageCommand# [ 5�      5�      �� 5�      $��Barcode Reader (for mPOP)��6   #��[ 68   �� 5�      #��[ 6   #��[ 6a   #��[ 6�   ��6J   Z� .@�5�      
  
 ��  � ' #GoBarcodeReaderExtSamplePageCommand# [ 5�       5�      �� # [ $! StarIOExtManagerSampleTitle�5�      5�      �� 5�      $&�� Display (for mPOP or TSP100IIIU)��6   #��[ 6?   �� 5�      #��[ 6   #��[ 6a   #��[ 6�   ��6J   Z� .@�5�      
  $ Sample��5�      
 ��  �  GoDisplaySamplePageCommand# [ 5�      5�      �� 5�      $��Scale (for mPOP)��6   #��[ 6/   �� 5�      #��[ 6   #��[ 6a   #��[ 6�   ��6J   Z� .@�5�      
  $ Sample��5�      
 ��  �  GoScaleSamplePageCommand# [ 5�      5�      �� 5�      $��Combination (for mPOP)��6   #��[ 65   �� 5�      #��[ 6   #��[ 6a   #��[ 6�   ��6J   Z� .@�5�      
  
 ��  � " GoCombinationSamplePageCommand# [ 5�       5�      �� # [ $! StarIOExtManagerSampleTitle�5�      5�      �� 5�      $&�� Combination (for Printer Driver)��6   #��[ 6?   �� 5�      #��[ 6   #��[ 6a   #��[ 6�   ��6J   Z� .@�5�      
  $ Sample��5�      
 ��  � / +GoCombinationPrinterDriverSamplePageCommand# [ 5�      5�      �� 5�      $	��API��6   #��[ 6"   �� 5�      #��[ 6   #��[ 6a   #��[ 6�   ��6J   Z� .@�5�      
  $ Sample��5�      
 ��  � 
  BackGroudColor
 �����  GoAPISamplePageCommand# [ 5�      5�      �� 5�      $��AllReceipts��6   #��[ 6*   �� 5�      #��[ 6   #��[ 6a   #��[ 6�   ��6J   Z� .@�5�      
  $ Sample��5�      
 ��  �
 ���� " GoAllReceiptsSamplePageCommand# [ 5�      5�      �� 5�      $��Device Status��6   #��[ 6,   �� 5�      #��[ 6   #��[ 6a   #��[ 6�   ��6J   Z� .@�5�      
  $ Sample��5�      
 ��  �
 ����� # GoDeviceStatusSamplePageCommand# [ 5�      
  5�      $ Serial Number��5�      
 ��  �  ShowSerialNumberCommand# [ 5�      5�      �� 5�      $��	Bluetooth��6   #��[ 6(   �� 5�      #��[ 6   #��[ 6a   #��[ 6�   ��6J   Z� .@�5�      
  $ Bluetooth Setting��5�      
 ��  � ' #GoBluetoothSettingSamplePageCommand# [ 5�      5�      �� 5�      $��Appendix��6   #��[ 6'   �� 5�      #��[ 6   #��[ 6a   #��[ 6�   ��6J   Z� .@�5�      
  $ Library Version��5�      
 ��  �   ShowLibraryVersionCommand# [  5�      5�      5�      5�      5�      