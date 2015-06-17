/*! @mainpage Documentation du Projet Rythmique
 *
 * @section intro_sec Introduction
 *
 * Projet regroupant Art et Réalité Virtuelle en collaboration avec l’association brestoise « Vivre Le Monde » (Body percussionnistes). 
 * Le but du projet est de permettre aux artistes de jouer de la musique en utilisant la reconnaissance de mouvement (wiimote).
 * 
 * <ul>
 * <li>Product Owner : DESMEULLES Gireg</li>
 * <li>Client : Association “Vivre le Monde”</li>
 * <li>Scrum Team : <ul>
 * <li>BIGEARD Charles-Henry</li>
 * <li>LEJEUNE Pierre</li>
 * <li>PÉRICÉ Robin</li></ul>
 * </ul>
 *
 * 
 * @section install_sec Utilisation
 *
 * @subsection step1 1 - L'interface
 * 
 * Lancez l'application à partir de l'exécutable. 
 * Au lancement de l'application un métronome apparait, il est constitué d'un cylindre dont la taille est fonction de la durée de la boucle ainsi que de sphères
 * représentants les sons de la boucle.
 * 
 * @image html capture_unity.png
 * 
 * @subsection step2 2 - Intéragir avec l'application
 * 
<p>Pour intéragir avec l'application il existe plusieurs méthodes (le clavier, les boutons dans l'ihm et la wiimote).</p>


<table style="width:auto" >
  <tr bgcolor="#23AEF6">
    <td><b>Intéraction</b></td>
    <td><b>Touche du clavier</b></td>
    <td><b>Bouton interface</b></td>
    <td><b>Bouton wiimote</b></td>
  </tr>
  <tr bgcolor="#F8F8F8">
    <td>Augmenter le nombre de BPM de la prochaine boucle créée</td>
    <td><center>t</center></td>
    <td><center>undefined</center></td>
     <td><center>undefined</center></td>
  </tr>
  <tr bgcolor="#F8F8F8">
    <td>Réduire le nombre de BPM de la prochaine boucle créée</td>
    <td><center>g</center></td>		
    <td><center>undefined</center></td>
    <td><center>undefined</center></td>
  </tr>
  <tr bgcolor="#F8F8F8">
    <td>Augmenter la mesure de la prochaine boucle créée</td>
    <td><center>y</center></td>
    <td><center>undefined</center></td>
    <td><center>undefined</center></td>
  </tr>
  <tr bgcolor="#F8F8F8">
    <td>Réduire la mesure de la prochaine boucle créée</td>
    <td><center>h</center></td>		
    <td><center>undefined</center></td>
    <td><center>undefined</center></td>
  </tr>
  <tr bgcolor="#F8F8F8">
    <td>Ajouter une boucle</td>
    <td><center>a</center></td>		
    <td><center>undefined</center></td>
    <td><center>undefined</center></td>
  </tr>
  <tr bgcolor="#F8F8F8">
    <td>Se déplacer vers la boucle de gauche</td>
    <td><center>flèche gauche</center></td>		
    <td><center>undefined</center></td>
    <td><center>flèche gauche</center></td>
  </tr>
  <tr bgcolor="#F8F8F8">
    <td>Se déplacer vers la boucle de droite</td>
    <td><center>flèche droite</center></td>		
    <td><center>undefined</center></td>
    <td><center>flèche droite</center></td>
  </tr>
  <tr bgcolor="#F8F8F8">
    <td>Ajouter un son de basse</td>
    <td><center>q</center></td>		
    <td><center>undefined</center></td>
    <td><center>A + mouvement wiimote</center></td>
  </tr>
  <tr bgcolor="#F8F8F8">
    <td>Ajouter un son de clave</td>
    <td><center>s</center></td>		
    <td><center>undefined</center></td>
    <td><center>B + mouvement wiimote</center></td>
  </tr>
  <tr bgcolor="#F8F8F8">
    <td>Ajouter un son de bip</td>
    <td><center>d</center></td>		
    <td><center>undefined</center></td>
    <td><center>A + B + mouvement wiimote</center></td>
  </tr>
    <tr bgcolor="#F8F8F8">
    <td>Mute/De-mute le métronome</td>
    <td><center>m</center></td>		
    <td><center>undefined</center></td>
    <td><center>undefined</center></td>
  </tr>  
  </tr>
    <tr bgcolor="#F8F8F8">
    <td>Supprimer une boucle</td>
    <td><center>x</center></td>   
    <td><center>undefined</center></td>
    <td><center>undefined</center></td>
  </tr>
</table>
 */