   M S B A M L   `   `   `  ����F  BStarPRNTSDK, Version=5.4.1.0, Culture=neutral, PublicKeyToken=null)clr-namespace:StarPRNTSDKStarPRNTSDK  5      4    .StarPRNTSDK.CombinationPrinterDriverSamplePage   5      R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      Hmc;http://schemas.openxmlformats.org/markup-compatibility/2006    5      >d2http://schemas.microsoft.com/expression/blend/2008    5      )localclr-namespace:StarPRNTSDK    5        N� Title$(  "CombinationPrinterDriverSamplePage��5	       � 	Resources 5      #   StarPRNTSDK.GoNextPageCommand 3  /GoPrinterDriverWithCheckStatusSamplePageCommand   NavigationUri   StarPRNTSDK.BaseCommand   
Executable4   .StarPRNTSDK.GoNextPageCommandWithSelectSetting 5 1GoPrinterDriverWithBarcodeReaderSamplePageCommand   ModelConfirmMessage    IsMaitainReceiptsSettings  SelectedModelManager �� Source �� Path �� Mode / +GoPrinterDriverWithDisplaySamplePageCommand - )GoPrinterDriverWithScaleSamplePageCommand��%�  &	        &	 Q     7  &	     7  &	 �    7    5   
   $1 +PrinterDriverWithCheckStatusSamplePage.xaml?�6Z    . 6�     5   
   $3 -PrinterDriverWithBarcodeReaderSamplePage.xaml?�6m   $ This menu is for mPOP��6�    .  6�   �� 8   $  BarcodeReaderSampleEnabled�$ OneWay=�  5   
   $- 'PrinterDriverWithDisplaySamplePage.xaml?�6g   $) #This menu is for mPOP or TSP100IIIU��6�    .  6�   �� 8   $ DisplaySampleEnabled�$ OneWay=�  5   
   $+ %PrinterDriverWithScaleSamplePage.xaml?�6e   $ This menu is for mPOP��6�    .  6�   �� 8   $ ScaleSampleEnabled�$ OneWay=�5      .��5      $   StarPRNTSDK.CustomScrollViewer  .��5   
   �� 	 �� Orientation$	 Vertical=�6   .+�5      ��   PageTitleStyle#��[ 6~   ��6   ��   ResourceManager# [ $- 'CombinationPrinterDriverSamplePageTitle�6~   �� 5        ListBoxSeparatorStyle#��[ 6r   ��6   �� # [ $! StarIOExtManagerSampleTitle�6r   �� 5        ListBoxItemTemplate#��[ 6    	 ListBoxStyle#��[	 6a    
 ListBoxItemStyle#��[
 6�   ��6J   Z� .@�5      !   StarPRNTSDK.BaseListBoxItem  
   Title$#
 Printer Driver + Check Status��5         ForeGroundColor
 ��  �   BorderBlushColor
 �����    ListBoxItemTouchedCommand# [  5         5"      $0
 *Printer Driver + Barcode Reader (for mPOP)��5#      
 ��  �
 �����# [ 5&        5(      $7
 1Printer Driver + Display (for mPOP or TSP100IIIU)��5)      
 ��  �
 �����# [ 5,        5.      $'
 !Printer Driver + Scale (for mPOP)��5/      
 ��  �
 �����# [ 52      54      56      57      58      