   M S B A M L   `   `   `  ����N  JKursnaListaGrafikon, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:KursnaListaGrafikonKursnaListaGrafikon  5   	   %    KursnaListaGrafikon.TableWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:KursnaListaGrafikon     5   	     � Title$  TableWindow��5   	   $	��450��6   $	��800��6*    � 	Resources 5	      &   System.Windows.Controls.DataGrid �� 
TargetType �� Property �� Value��%   '        �� 5   
   "  6   .�5      ��  � ��$ 10q�6'   �� 5       � ��
 ���� 6+   �� 5       �@ RowBackground
 ���� 6.   �� 5       � �� �#5865706,   �� 5       � �� �#e9d9d16+   5      5      .��5      � ���#16262e.+�5   
     -   5        $��tabela��5         AutoGenerateColumns .  ��5      �� *podaciTabele+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 
Par valuta��5   %      Width5  /System.Windows.Controls.DataGridLengthConverter$
 0.2* 5   %   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 5   %   �� *par+5   %     5      $ Datum i vreme��5   %   $
 0.2* 5   %   	 5   %   �� *date+5   %     5       $
 Open��5!   %   $
 0.2* 5"   %   	 5    %   �� *open+5"   %     5#      $	 Low��5$   %   $
 0.2* 5%   %   	 5#   %   �� *low+5%   %     5&      $
 High��5'   %   $
 0.2* 5(   %   	 5&   %   �� *high+5(   %     5)      $ Close��5*   %   $
 0.2* 5+   %   	 5)   %   �� *close+5+   %    5/      =  7System.Windows.Controls.Primitives.DataGridColumnHeader��%&  '        �� 50      "  6   .�51      ��  � �� �#e9d9d163   �� 52       � �� �#16262e63   �� 53       � ��$ Stretch=�6<   �� 54       � ��$ 25��6/   �� 55       � ��$ DemiBold"�64   �� 56       � ��$ Center=�6C   6T   57      58      
   	CellStyle
 59      �� 5:      *  $System.Windows.Controls.DataGridCell"  6   .�5;      ��  � �� �#58657064   �� 5<       � ��$	 0.6q�68   �� 5=       � ��$ Stretch=�6<   �� 5>       � ��$ Center=�6C   5?      5B      5C      5D      