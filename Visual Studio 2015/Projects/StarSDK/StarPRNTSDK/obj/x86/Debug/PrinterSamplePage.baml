   M S B A M L   `   `   `  ����F  BStarPRNTSDK, Version=5.4.1.0, Culture=neutral, PublicKeyToken=null)clr-namespace:StarPRNTSDKStarPRNTSDK  5      #    StarPRNTSDK.PrinterSamplePage   5      R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      Hmc;http://schemas.openxmlformats.org/markup-compatibility/2006    5      >d2http://schemas.microsoft.com/expression/blend/2008    5      )localclr-namespace:StarPRNTSDK    5        N� Title$  PrinterSamplePage��5	       � 	Resources 5      0   *StarPRNTSDK.PrintLocalizeReceiptClickEvent   PrintTextReceiptCommand  TextReceipt    ReceiptInformationManager   StarPRNTSDK.BaseCommand   
Executable  SelectedModelManager �� Source �� Path �� Mode  PrintTextReceiptUTF8Command  TextReceiptUTF8  PrintRasterReceiptCommand  RasterReceipt & "PrintRasterReceiptBothScaleCommand  RasterReceiptBothScale "	 PrintRasterReceiptScaleCommand 
 RasterReceiptScale  PrintRasterCouponCommand  RasterCoupon & "PrintRasterCouponRotation90Command  RasterCouponRotation901   +StarPRNTSDK.PrintPhotoFromLibraryClickEvent   PrintPhotoFromLibraryCommand  ReceiptInformationManager    ReceiptInformationManager5   /StarPRNTSDK.GoNextPageWithSetReceiptTypeCommand  GoPrinterExtPageTextCommand   ReceiptType#   StarPRNTSDK.GoNextPageCommand	   NavigationUri # GoPrinterExtPageTextUTF8Command ! GoPrinterExtPageRasterCommand * &GoPrinterExtPageRasterBothScaleCommand & "GoPrinterExtPageRasterScaleCommand ' #GoPrinterExtPageRasterCouponCommand 1 -GoPrinterExtPageRasterCouponRotation90Command��%�  &	        7  7  &	 L     7  7  &	 �     7  7  &	 �     7  7  &		 8    7 
 7  &	 �    7  7  &	 �    7  7  &	      7  &	 ?    7  &	 �    7  &	 ?    7  &	 �    7  &	 H    7  &	 �    7  &	 R    7    5   
   8    6�   �� 8  $ TextReceiptEnabled�$ OneWay=�  5   
   8    6�   �� 8  $ TextUTF8ReceiptEnabled�$ OneWay=�  5   
   8    6�   �� 8  $ RasterReceiptEnabled�$ OneWay=�  5   
   8    6�   �� 8  $ RasterReceiptEnabled�$ OneWay=�  5   
   8    6�   �� 8  $ RasterReceiptEnabled�$ OneWay=�  5   
   8    6�   �� 8  $ RasterCouponEnabled�$ OneWay=�  5   
   8    6�   �� 8  $ RasterCouponEnabled�$ OneWay=�  5   
   8    . 6�     5   
   $
 Text=�6X   $	 PrinterExtSamplePage.xaml?�6k    6�   �� 8   $ TextReceiptEnabled�$ OneWay=�  5   
   $ TextUTF8=�6\   $	 PrinterExtSamplePage.xaml?�6s    6�   �� 8   $ TextUTF8ReceiptEnabled�$ OneWay=�  5   
   $ Raster=�6Z   $	 PrinterExtSamplePage.xaml?�6o    6�   �� 8   $ RasterReceiptEnabled�$ OneWay=�  5   
   $ RasterBothScale=�6c   $	 PrinterExtSamplePage.xaml?�6�    6�   �� 8   $ RasterReceiptEnabled�$ OneWay=�  5   
   $ RasterScale=�6_   $	 PrinterExtSamplePage.xaml?�6y    6�   �� 8   $ RasterReceiptEnabled�$ OneWay=�  5   
   $ RasterCoupon=�6`   $	 PrinterExtSamplePage.xaml?�6{    6�   �� 8   $ RasterCouponEnabled�$ OneWay=�  5   
   $ RasterCouponRotation90=�6j   $	 PrinterExtSamplePage.xaml?�6�    6�   �� 8   $ RasterCouponEnabled�$ OneWay=�5      .��5      $   StarPRNTSDK.CustomScrollViewer  .��5    
   �� 
 �� Orientation$
 Vertical=�6   .+�5!      ��   PageTitleStyle#��[ 6m   ��6   ��   ResourceManager# [ $ PrinterSamplePageTitle�6m   �� 5#        ListBoxSeparatorStyle#��[ 6k   ��6   �� # [ $ StarIOSDKSampleTitle�6k   �� 5%        ListBoxItemTemplate#��[ 6     ListBoxStyle#��[ 6a     ListBoxItemStyle#��[ 6�   ��6J   Z� .@�5&      !   StarPRNTSDK.BaseListBoxItem     ForeGroundColor
 ��  �   BorderBlushColor
 �����    ListBoxItemTouchedCommand# [  5*         Title 5'      �� # [ $ Description�5*        5,      
 ��  �
 �����# [ 50       5-      �� # [ $ Description�50        52      
 ��  �
 �����# [ 56       53      �� # [ $ Description�56        58      
 ��  �
 �����# [ 5<       59      �� # [ $ Description�5<        5>      
 ��  �
 �����# [	 5B       5?      �� # [
 $ Description�5B        5D      
 ��  �
 �����# [ 5H       5E      �� # [ $ Description�5H        5J      
 ��  �# [ 5M       5K      �� # [ $ Description�5M      5N      �� 5P      #��[ 6r   ��6   �� # [ $! StarIOExtManagerSampleTitle�6r   �� 5R      #��[ 6   #��[ 6a   #��[ 6�   ��6J   Z� .@�5S        
 ��  �
 �����# [ 5W       5T      �� # [ $ Description�5W        5Y      
 ��  �
 �����# [ 5]       5Z      �� # [ $ Description�5]        5_      
 ��  �
 �����# [ 5c       5`      �� # [ $ Description�5c        5e      
 ��  �
 �����# [ 5i       5f      �� # [ $ Description�5i        5k      
 ��  �
 �����# [ 5o       5l      �� # [
 $ Description�5o        5q      
 ��  �
 �����# [ 5u       5r      �� # [ $ Description�5u        5w      
 ��  �# [ 5z       5x      �� # [ $ Description�5z      5{      �� 5}      $��Appendix��6   #��[ 6'   �� 5~      #��[ 6   #��[ 6a   #��[ 6�   ��6J   Z� .@�5        $ Print Photo from Library��5�      
 ��  �
 �����# [ 5�      5�      5�      5�      5�      