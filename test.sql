CREATE DATABASE [best_resturant_test]GO��U S E   [ b e s t _ r e s t u r a n t _ t e s t ]  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ c u i s i n e ]         S c r i p t   D a t e :   2 / 2 5 / 2 0 1 6   2 : 2 8 : 2 8   P M   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 S E T   A N S I _ P A D D I N G   O N  
 G O  
 C R E A T E   T A B L E   [ d b o ] . [ c u i s i n e ] (  
 	 [ i d ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ n a m e ]   [ v a r c h a r ] ( 2 5 5 )   N U L L  
 )   O N   [ P R I M A R Y ]  
  
 G O  
 S E T   A N S I _ P A D D I N G   O F F  
 G O  
 / * * * * * *   O b j e c t :     T a b l e   [ d b o ] . [ r e s t u r a n t s ]         S c r i p t   D a t e :   2 / 2 5 / 2 0 1 6   2 : 2 8 : 2 8   P M   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 S E T   A N S I _ P A D D I N G   O N  
 G O  
 C R E A T E   T A B L E   [ d b o ] . [ r e s t u r a n t s ] (  
 	 [ i d ]   [ i n t ]   I D E N T I T Y ( 1 , 1 )   N O T   N U L L ,  
 	 [ n a m e ]   [ v a r c h a r ] ( 2 5 5 )   N U L L ,  
 	 [ d a t e ]   [ d a t e t i m e ]   N U L L ,  
 	 [ l o c a t i o n ]   [ v a r c h a r ] ( 2 5 5 )   N U L L ,  
 	 [ c u i s i n e _ i d ]   [ i n t ]   N U L L  
 )   O N   [ P R I M A R Y ]  
  
 G O  
 S E T   A N S I _ P A D D I N G   O F F  
 G O  
 
