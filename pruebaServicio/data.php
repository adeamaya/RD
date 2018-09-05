<?php
class ArrayValue implements JsonSerializable {
    public function __construct(array $array) {
        $this->array = $array;
    }

    public function jsonSerialize() {
        return $this->array;
    }
}

function isValidJSON($str) {
   json_decode($str);
   return json_last_error() == JSON_ERROR_NONE;
}

$json_params = file_get_contents("php://input");

if (strlen($json_params) > 0 && isValidJSON($json_params))
  $decoded_params = json_decode($json_params,true);

$consulta = $decoded_params['tipoConsulta'];


if($consulta == 'Revista')
{
	$IdRevista = $decoded_params['IdRevista'];
	$contenidoImagen = file_get_contents('imagenes/logo.png');
	$LogoImagenBase64 = base64_encode($contenidoImagen);
	$array = array('IdRevista' => '1','Nombre' => 'Nombre Revista', 'LogoBlob' => $LogoImagenBase64, 'WebInstitucional' => 'https://www.icbf.gov.co/', 'Version' => '1');
}

if($consulta == 'Ediciones')
{
	$IdRevista = $decoded_params['IdRevista'];
	$contenidoImagen = file_get_contents('imagenes/Portada1.jpg');
	$ImagenArt1 = base64_encode($contenidoImagen);
	$contenidoImagen = file_get_contents('imagenes/Portada2.jpg');
	$ImagenArt2 = base64_encode($contenidoImagen);
		
	$array = array( array('IdEdicion' => '3','NumeroEdicion' => '3', 'Titulo' => 'Titulo de la edicion', 'Descripcion' => 'Descripcion de la edicion','ImagenBlob' => $ImagenArt1,'Version' => '1','Fecha' => '2018/08/01'),
					array('IdEdicion' => '4','NumeroEdicion' => '5', 'Titulo' => 'Titulo de la edicion', 'Descripcion' => 'Descripcion de la edicion','ImagenBlob' => $ImagenArt2,'Version' => '1','Fecha' => '2018/08/01')
				   );	
}

if($consulta == 'UltimaEdicion')
{
	$IdEdicion = $decoded_params['IdEdicion'];
	$contenidoImagen = file_get_contents('imagenes/Portada1.jpg');
	$PortadaImagenBase64 = base64_encode($contenidoImagen);
	$array = ['IdEdicion' => '3','NumeroEdicion' => '3', 'Titulo' => 'Titulo de la edicion', 'Descripcion' => 'Descripcion de la edicion','ImagenBlob' => $PortadaImagenBase64,'Version' => '1','Fecha' => '2018/08/01'];
}

if($consulta == 'Edicion')
{
	$IdEdicion = $decoded_params['IdEdicion'];
	$contenidoImagen = file_get_contents('imagenes/Portada1.jpg');
	$PortadaImagenBase64_1 = base64_encode($contenidoImagen);
	$contenidoImagen = file_get_contents('imagenes/Portada2.jpg');
	$PortadaImagenBase64_2 = base64_encode($contenidoImagen);
		
	if($IdEdicion==3){
		$array = array('IdEdicion' => '3','NumeroEdicion' => '3', 'Titulo' => 'Titulo de la edicion 3', 'Descripcion' => 'Descripcion de la edicion 3','ImagenBlob' => $PortadaImagenBase64_1,'Version' => '1','Fecha' => '2018/08/01');
	}	
	if($IdEdicion==4){
		$array = array('IdEdicion' => '4','NumeroEdicion' => '5', 'Titulo' => 'Titulo de la edicion 5', 'Descripcion' => 'Descripcion de la edicion 5','ImagenBlob' => $PortadaImagenBase64_2,'Version' => '1','Fecha' => '2018/08/01');
	}
}


if($consulta == 'ArticulosEdicion')
{
	$Id_Edicion = $decoded_params['IdEdicion'];
	$contenidoImagen = file_get_contents('imagenes/Articulo1.jpg');
	$ImagenArt1 = base64_encode($contenidoImagen);
	$contenidoImagen = file_get_contents('imagenes/Articulo2.jpg');
	$ImagenArt2 = base64_encode($contenidoImagen);
		  
	if($Id_Edicion==3)
	{
		$array = array( array('IdEdicion' => '3', 'IdArticulo' => '1','Titulo' => 'Titulo del articulo 1','ImagenBlob' => $ImagenArt1,'Fecha' => '08/08/2018','Tipo' => 'Articulos','Visualizaciones' => '10','Version' => '1','Texto' => '<p>Lorem ipsum dolor sit amet consectetur adipiscing, elit morbi vehicula vivamus aptent dictumst tellus, diam cras pellentesque ornare tempus. Imperdiet ut litora auctor lectus justo porttitor, enim sapien dapibus libero tempor mauris, curae dignissim accumsan facilisi arcu. In sapien praesent eros ultrices nec posuere egestas scelerisque fermentum, laoreet augue fusce vitae suscipit ornare ad tincidunt aenean arcu, ultricies conubia nam dictum eget vel proin cursus.</p><p>Iaculis tellus mi massa lectus ligula aptent dis, venenatis nec ante fusce nam dignissim vehicula, habitasse erat mauris viverra natoque urna. Pellentesque penatibus facilisis aliquet tempor suscipit, in himenaeos orci nisl dis parturient, varius mattis eros euismod. A accumsan quisque duis hendrerit nullam sagittis etiam volutpat ultrices ultricies, rutrum iaculis laoreet urna eget lobortis lacinia congue inceptos dictum, interdum facilisis in odio ac erat malesuada mauris mus.</p>'),
					array('IdEdicion' => '3', 'IdArticulo' => '2','Titulo' => 'Titulo del articulo 2','ImagenBlob' => $ImagenArt2,'Fecha' => '09/08/2018','Tipo' => 'Noticias','Visualizaciones' => '20','Version' => '1','Texto' => '<p>Lorem ipsum dolor sit amet consectetur adipiscing, elit morbi vehicula vivamus aptent dictumst tellus, diam cras pellentesque ornare tempus. Imperdiet ut litora auctor lectus justo porttitor, enim sapien dapibus libero tempor mauris, curae dignissim accumsan facilisi arcu. In sapien praesent eros ultrices nec posuere egestas scelerisque fermentum, laoreet augue fusce vitae suscipit ornare ad tincidunt aenean arcu, ultricies conubia nam dictum eget vel proin cursus.</p><p>Iaculis tellus mi massa lectus ligula aptent dis, venenatis nec ante fusce nam dignissim vehicula, habitasse erat mauris viverra natoque urna. Pellentesque penatibus facilisis aliquet tempor suscipit, in himenaeos orci nisl dis parturient, varius mattis eros euismod. A accumsan quisque duis hendrerit nullam sagittis etiam volutpat ultrices ultricies, rutrum iaculis laoreet urna eget lobortis lacinia congue inceptos dictum, interdum facilisis in odio ac erat malesuada mauris mus.</p>')
				   );	
	}
	
	if($Id_Edicion==4)
	{
		$array = array( array('IdEdicion' => '4', 'IdArticulo' => '1','Titulo' => 'Titulo del articulo 1 -Edicion4','ImagenBlob' => $ImagenArt1,'Fecha' => '08/08/2018','Tipo' => 'Articulos','Visualizaciones' => '10','Version' => '1','Texto' => '<p>Lorem ipsum dolor sit amet consectetur adipiscing, elit morbi vehicula vivamus aptent dictumst tellus, diam cras pellentesque ornare tempus. Imperdiet ut litora auctor lectus justo porttitor, enim sapien dapibus libero tempor mauris, curae dignissim accumsan facilisi arcu. In sapien praesent eros ultrices nec posuere egestas scelerisque fermentum, laoreet augue fusce vitae suscipit ornare ad tincidunt aenean arcu, ultricies conubia nam dictum eget vel proin cursus.</p><p>Iaculis tellus mi massa lectus ligula aptent dis, venenatis nec ante fusce nam dignissim vehicula, habitasse erat mauris viverra natoque urna. Pellentesque penatibus facilisis aliquet tempor suscipit, in himenaeos orci nisl dis parturient, varius mattis eros euismod. A accumsan quisque duis hendrerit nullam sagittis etiam volutpat ultrices ultricies, rutrum iaculis laoreet urna eget lobortis lacinia congue inceptos dictum, interdum facilisis in odio ac erat malesuada mauris mus.</p>'),
					array('IdEdicion' => '4', 'IdArticulo' => '2','Titulo' => 'Titulo del articulo 2 - Edicion4','ImagenBlob' => $ImagenArt2,'Fecha' => '09/08/2018','Tipo' => 'Noticias','Visualizaciones' => '20','Version' => '1','Texto' => '<p>Lorem ipsum dolor sit amet consectetur adipiscing, elit morbi vehicula vivamus aptent dictumst tellus, diam cras pellentesque ornare tempus. Imperdiet ut litora auctor lectus justo porttitor, enim sapien dapibus libero tempor mauris, curae dignissim accumsan facilisi arcu. In sapien praesent eros ultrices nec posuere egestas scelerisque fermentum, laoreet augue fusce vitae suscipit ornare ad tincidunt aenean arcu, ultricies conubia nam dictum eget vel proin cursus.</p><p>Iaculis tellus mi massa lectus ligula aptent dis, venenatis nec ante fusce nam dignissim vehicula, habitasse erat mauris viverra natoque urna. Pellentesque penatibus facilisis aliquet tempor suscipit, in himenaeos orci nisl dis parturient, varius mattis eros euismod. A accumsan quisque duis hendrerit nullam sagittis etiam volutpat ultrices ultricies, rutrum iaculis laoreet urna eget lobortis lacinia congue inceptos dictum, interdum facilisis in odio ac erat malesuada mauris mus.</p>')
				   );	
	}
	
}


if($consulta == 'Articulo')
{
	$Id_Articulo = $decoded_params['IdArticulo'];
	$contenidoImagen = file_get_contents('imagenes/Articulo1.jpg');
	$ImagenArt1 = base64_encode($contenidoImagen);
	$contenidoImagen = file_get_contents('imagenes/Articulo2.jpg');
	$ImagenArt2 = base64_encode($contenidoImagen);
		  
	if($Id_Articulo==1)
	{
		$array = array('IdEdicion' => '3', 'IdArticulo' => '1','Titulo' => 'Titulo del articulo 1','ImagenBlob' => $ImagenArt1,'Fecha' => '08/08/2018','Tipo' => 'Articulos','Visualizaciones' => '10','Version' => '1','Texto' => '<p>Lorem ipsum dolor sit amet consectetur adipiscing, elit morbi vehicula vivamus aptent dictumst tellus, diam cras pellentesque ornare tempus. Imperdiet ut litora auctor lectus justo porttitor, enim sapien dapibus libero tempor mauris, curae dignissim accumsan facilisi arcu. In sapien praesent eros ultrices nec posuere egestas scelerisque fermentum, laoreet augue fusce vitae suscipit ornare ad tincidunt aenean arcu, ultricies conubia nam dictum eget vel proin cursus.</p><p>Iaculis tellus mi massa lectus ligula aptent dis, venenatis nec ante fusce nam dignissim vehicula, habitasse erat mauris viverra natoque urna. Pellentesque penatibus facilisis aliquet tempor suscipit, in himenaeos orci nisl dis parturient, varius mattis eros euismod. A accumsan quisque duis hendrerit nullam sagittis etiam volutpat ultrices ultricies, rutrum iaculis laoreet urna eget lobortis lacinia congue inceptos dictum, interdum facilisis in odio ac erat malesuada mauris mus.</p>');	
	}
	
	if($Id_Articulo==2)
	{
		$array = array('IdEdicion' => '4', 'IdArticulo' => '2','Titulo' => 'Titulo del articulo 2 - Edicion4','ImagenBlob' => $ImagenArt2,'Fecha' => '09/08/2018','Tipo' => 'Noticias','Visualizaciones' => '20','Version' => '1','Texto' => '<p>Lorem ipsum dolor sit amet consectetur adipiscing, elit morbi vehicula vivamus aptent dictumst tellus, diam cras pellentesque ornare tempus. Imperdiet ut litora auctor lectus justo porttitor, enim sapien dapibus libero tempor mauris, curae dignissim accumsan facilisi arcu. In sapien praesent eros ultrices nec posuere egestas scelerisque fermentum, laoreet augue fusce vitae suscipit ornare ad tincidunt aenean arcu, ultricies conubia nam dictum eget vel proin cursus.</p><p>Iaculis tellus mi massa lectus ligula aptent dis, venenatis nec ante fusce nam dignissim vehicula, habitasse erat mauris viverra natoque urna. Pellentesque penatibus facilisis aliquet tempor suscipit, in himenaeos orci nisl dis parturient, varius mattis eros euismod. A accumsan quisque duis hendrerit nullam sagittis etiam volutpat ultrices ultricies, rutrum iaculis laoreet urna eget lobortis lacinia congue inceptos dictum, interdum facilisis in odio ac erat malesuada mauris mus.</p>');	
	}	
}



if($consulta == 'Regional')
{
	$Id_Regional = $decoded_params['IdRegional'];
	$contenidoImagen = file_get_contents('imagenes/Regional1.png');
	$ImagenReg1 = base64_encode($contenidoImagen);
	$contenidoImagen = file_get_contents('imagenes/Regional2.png');
	$ImagenReg2 = base64_encode($contenidoImagen);
		  
	if($Id_Regional==3)
	{
		$array = array('IdRegional' => '3','Nombre' => 'Regional 1', 'ImagenBlob' => $ImagenReg1,'Version' => '1');	
	}
	
	if($Id_Regional==4)
	{
		$array = array('IdRegional' => '4','Nombre' => 'Regional 2', 'ImagenBlob' => $ImagenReg2,'Version' => '1');
	}	
}

if($consulta == 'Regionales')
{
	$contenidoImagen = file_get_contents('imagenes/Regional1.png');
	$ImagenReg1 = base64_encode($contenidoImagen);
	$contenidoImagen = file_get_contents('imagenes/Regional2.png');
	$ImagenReg2 = base64_encode($contenidoImagen);
		
	$array = array( array('IdRegional' => '3','Nombre' => 'Regional 1', 'ImagenBlob' => $ImagenReg1,'Version' => '1'),
					array('IdRegional' => '4','Nombre' => 'Regional 2', 'ImagenBlob' => $ImagenReg2,'Version' => '1')
				   );	
}



if($consulta == 'ArticulosRegional')
{
	$Id_Regional = $decoded_params['IdRegional'];
	$contenidoImagen = file_get_contents('imagenes/Articulo1.jpg');
	$ImagenArt1 = base64_encode($contenidoImagen);
	$contenidoImagen = file_get_contents('imagenes/Articulo2.jpg');
	$ImagenArt2 = base64_encode($contenidoImagen);
		  
	if($Id_Regional==3)
	{
		$array = array( array('IdEdicion' => '3', 'IdArticulo' => '1','Titulo' => 'Titulo del articulo 1 Regional 1','ImagenBlob' => $ImagenArt1,'Fecha' => '08/08/2018','Tipo' => 'Articulos','Visualizaciones' => '10','Version' => '1','Texto' => '<p>Lorem ipsum dolor sit amet consectetur adipiscing, elit morbi vehicula vivamus aptent dictumst tellus, diam cras pellentesque ornare tempus. Imperdiet ut litora auctor lectus justo porttitor, enim sapien dapibus libero tempor mauris, curae dignissim accumsan facilisi arcu. In sapien praesent eros ultrices nec posuere egestas scelerisque fermentum, laoreet augue fusce vitae suscipit ornare ad tincidunt aenean arcu, ultricies conubia nam dictum eget vel proin cursus.</p><p>Iaculis tellus mi massa lectus ligula aptent dis, venenatis nec ante fusce nam dignissim vehicula, habitasse erat mauris viverra natoque urna. Pellentesque penatibus facilisis aliquet tempor suscipit, in himenaeos orci nisl dis parturient, varius mattis eros euismod. A accumsan quisque duis hendrerit nullam sagittis etiam volutpat ultrices ultricies, rutrum iaculis laoreet urna eget lobortis lacinia congue inceptos dictum, interdum facilisis in odio ac erat malesuada mauris mus.</p>'),
					array('IdEdicion' => '3', 'IdArticulo' => '2','Titulo' => 'Titulo del articulo 2 Regional 1','ImagenBlob' => $ImagenArt2,'Fecha' => '09/08/2018','Tipo' => 'Noticias','Visualizaciones' => '20','Version' => '1','Texto' => '<p>Lorem ipsum dolor sit amet consectetur adipiscing, elit morbi vehicula vivamus aptent dictumst tellus, diam cras pellentesque ornare tempus. Imperdiet ut litora auctor lectus justo porttitor, enim sapien dapibus libero tempor mauris, curae dignissim accumsan facilisi arcu. In sapien praesent eros ultrices nec posuere egestas scelerisque fermentum, laoreet augue fusce vitae suscipit ornare ad tincidunt aenean arcu, ultricies conubia nam dictum eget vel proin cursus.</p><p>Iaculis tellus mi massa lectus ligula aptent dis, venenatis nec ante fusce nam dignissim vehicula, habitasse erat mauris viverra natoque urna. Pellentesque penatibus facilisis aliquet tempor suscipit, in himenaeos orci nisl dis parturient, varius mattis eros euismod. A accumsan quisque duis hendrerit nullam sagittis etiam volutpat ultrices ultricies, rutrum iaculis laoreet urna eget lobortis lacinia congue inceptos dictum, interdum facilisis in odio ac erat malesuada mauris mus.</p>')
				   );	
	}
	
	if($Id_Regional==4)
	{
		$array = array( array('IdEdicion' => '4', 'IdArticulo' => '1','Titulo' => 'Titulo del articulo 1 -Regional 2','ImagenBlob' => $ImagenArt1,'Fecha' => '08/08/2018','Tipo' => 'Articulos','Visualizaciones' => '10','Version' => '1','Texto' => '<p>Lorem ipsum dolor sit amet consectetur adipiscing, elit morbi vehicula vivamus aptent dictumst tellus, diam cras pellentesque ornare tempus. Imperdiet ut litora auctor lectus justo porttitor, enim sapien dapibus libero tempor mauris, curae dignissim accumsan facilisi arcu. In sapien praesent eros ultrices nec posuere egestas scelerisque fermentum, laoreet augue fusce vitae suscipit ornare ad tincidunt aenean arcu, ultricies conubia nam dictum eget vel proin cursus.</p><p>Iaculis tellus mi massa lectus ligula aptent dis, venenatis nec ante fusce nam dignissim vehicula, habitasse erat mauris viverra natoque urna. Pellentesque penatibus facilisis aliquet tempor suscipit, in himenaeos orci nisl dis parturient, varius mattis eros euismod. A accumsan quisque duis hendrerit nullam sagittis etiam volutpat ultrices ultricies, rutrum iaculis laoreet urna eget lobortis lacinia congue inceptos dictum, interdum facilisis in odio ac erat malesuada mauris mus.</p>'),
					array('IdEdicion' => '4', 'IdArticulo' => '2','Titulo' => 'Titulo del articulo 2 - Regional 2','ImagenBlob' => $ImagenArt2,'Fecha' => '09/08/2018','Tipo' => 'Noticias','Visualizaciones' => '20','Version' => '1','Texto' => '<p>Lorem ipsum dolor sit amet consectetur adipiscing, elit morbi vehicula vivamus aptent dictumst tellus, diam cras pellentesque ornare tempus. Imperdiet ut litora auctor lectus justo porttitor, enim sapien dapibus libero tempor mauris, curae dignissim accumsan facilisi arcu. In sapien praesent eros ultrices nec posuere egestas scelerisque fermentum, laoreet augue fusce vitae suscipit ornare ad tincidunt aenean arcu, ultricies conubia nam dictum eget vel proin cursus.</p><p>Iaculis tellus mi massa lectus ligula aptent dis, venenatis nec ante fusce nam dignissim vehicula, habitasse erat mauris viverra natoque urna. Pellentesque penatibus facilisis aliquet tempor suscipit, in himenaeos orci nisl dis parturient, varius mattis eros euismod. A accumsan quisque duis hendrerit nullam sagittis etiam volutpat ultrices ultricies, rutrum iaculis laoreet urna eget lobortis lacinia congue inceptos dictum, interdum facilisis in odio ac erat malesuada mauris mus.</p>')
				   );	
	}
	
}

if($consulta == 'Favoritos')
{	
	$contenidoImagen = file_get_contents('imagenes/Articulo1.jpg');
	$ImagenArt1 = base64_encode($contenidoImagen);
	$contenidoImagen = file_get_contents('imagenes/Articulo2.jpg');
	$ImagenArt2 = base64_encode($contenidoImagen);
		  
$array = array( array('IdEdicion' => '3', 'IdArticulo' => '1','Titulo' => 'Titulo del articulo 1 Regional 1','ImagenBlob' => $ImagenArt1,'Fecha' => '08/08/2018','Tipo' => 'Articulos','Visualizaciones' => '10','Version' => '1','Texto' => '<p>Lorem ipsum dolor sit amet consectetur adipiscing, elit morbi vehicula vivamus aptent dictumst tellus, diam cras pellentesque ornare tempus. Imperdiet ut litora auctor lectus justo porttitor, enim sapien dapibus libero tempor mauris, curae dignissim accumsan facilisi arcu. In sapien praesent eros ultrices nec posuere egestas scelerisque fermentum, laoreet augue fusce vitae suscipit ornare ad tincidunt aenean arcu, ultricies conubia nam dictum eget vel proin cursus.</p><p>Iaculis tellus mi massa lectus ligula aptent dis, venenatis nec ante fusce nam dignissim vehicula, habitasse erat mauris viverra natoque urna. Pellentesque penatibus facilisis aliquet tempor suscipit, in himenaeos orci nisl dis parturient, varius mattis eros euismod. A accumsan quisque duis hendrerit nullam sagittis etiam volutpat ultrices ultricies, rutrum iaculis laoreet urna eget lobortis lacinia congue inceptos dictum, interdum facilisis in odio ac erat malesuada mauris mus.</p>'),
					array('IdEdicion' => '3', 'IdArticulo' => '2','Titulo' => 'Titulo del articulo 2 Regional 1','ImagenBlob' => $ImagenArt2,'Fecha' => '09/08/2018','Tipo' => 'Noticias','Visualizaciones' => '20','Version' => '1','Texto' => '<p>Lorem ipsum dolor sit amet consectetur adipiscing, elit morbi vehicula vivamus aptent dictumst tellus, diam cras pellentesque ornare tempus. Imperdiet ut litora auctor lectus justo porttitor, enim sapien dapibus libero tempor mauris, curae dignissim accumsan facilisi arcu. In sapien praesent eros ultrices nec posuere egestas scelerisque fermentum, laoreet augue fusce vitae suscipit ornare ad tincidunt aenean arcu, ultricies conubia nam dictum eget vel proin cursus.</p><p>Iaculis tellus mi massa lectus ligula aptent dis, venenatis nec ante fusce nam dignissim vehicula, habitasse erat mauris viverra natoque urna. Pellentesque penatibus facilisis aliquet tempor suscipit, in himenaeos orci nisl dis parturient, varius mattis eros euismod. A accumsan quisque duis hendrerit nullam sagittis etiam volutpat ultrices ultricies, rutrum iaculis laoreet urna eget lobortis lacinia congue inceptos dictum, interdum facilisis in odio ac erat malesuada mauris mus.</p>')
				   );
	//$array = array();			   
}


if($consulta == 'Destacados')
{	
	$contenidoImagen = file_get_contents('imagenes/Articulo1.jpg');
	$ImagenArt1 = base64_encode($contenidoImagen);
	$contenidoImagen = file_get_contents('imagenes/Articulo2.jpg');
	$ImagenArt2 = base64_encode($contenidoImagen);
		  
	$array = array( array('IdEdicion' => '3', 'IdArticulo' => '1','Titulo' => 'Titulo del articulo 1 Regional 1','ImagenBlob' => $ImagenArt1,'Fecha' => '08/08/2018','Tipo' => 'Articulos','Visualizaciones' => '10','Version' => '1','Texto' => '<p>Lorem ipsum dolor sit amet consectetur adipiscing, elit morbi vehicula vivamus aptent dictumst tellus, diam cras pellentesque ornare tempus. Imperdiet ut litora auctor lectus justo porttitor, enim sapien dapibus libero tempor mauris, curae dignissim accumsan facilisi arcu. In sapien praesent eros ultrices nec posuere egestas scelerisque fermentum, laoreet augue fusce vitae suscipit ornare ad tincidunt aenean arcu, ultricies conubia nam dictum eget vel proin cursus.</p><p>Iaculis tellus mi massa lectus ligula aptent dis, venenatis nec ante fusce nam dignissim vehicula, habitasse erat mauris viverra natoque urna. Pellentesque penatibus facilisis aliquet tempor suscipit, in himenaeos orci nisl dis parturient, varius mattis eros euismod. A accumsan quisque duis hendrerit nullam sagittis etiam volutpat ultrices ultricies, rutrum iaculis laoreet urna eget lobortis lacinia congue inceptos dictum, interdum facilisis in odio ac erat malesuada mauris mus.</p>'),
					array('IdEdicion' => '3', 'IdArticulo' => '2','Titulo' => 'Titulo del articulo 2 Regional 1','ImagenBlob' => $ImagenArt2,'Fecha' => '09/08/2018','Tipo' => 'Noticias','Visualizaciones' => '20','Version' => '1','Texto' => '<p>Lorem ipsum dolor sit amet consectetur adipiscing, elit morbi vehicula vivamus aptent dictumst tellus, diam cras pellentesque ornare tempus. Imperdiet ut litora auctor lectus justo porttitor, enim sapien dapibus libero tempor mauris, curae dignissim accumsan facilisi arcu. In sapien praesent eros ultrices nec posuere egestas scelerisque fermentum, laoreet augue fusce vitae suscipit ornare ad tincidunt aenean arcu, ultricies conubia nam dictum eget vel proin cursus.</p><p>Iaculis tellus mi massa lectus ligula aptent dis, venenatis nec ante fusce nam dignissim vehicula, habitasse erat mauris viverra natoque urna. Pellentesque penatibus facilisis aliquet tempor suscipit, in himenaeos orci nisl dis parturient, varius mattis eros euismod. A accumsan quisque duis hendrerit nullam sagittis etiam volutpat ultrices ultricies, rutrum iaculis laoreet urna eget lobortis lacinia congue inceptos dictum, interdum facilisis in odio ac erat malesuada mauris mus.</p>')
				   );
	//$array = array();			   
}

if($consulta == 'Editorial')
{	
	$contenidoImagen = file_get_contents('imagenes/Articulo1.jpg');
	$ImagenArt1 = base64_encode($contenidoImagen);	
		  
	$array = array('IdEdicion' => '3', 'IdArticulo' => '1','Titulo' => 'Editorial de la ultima edicion','ImagenBlob' => $ImagenArt1,'Fecha' => '08/08/2018','Tipo' => 'Editorial','Visualizaciones' => '20','Version' => '1','Texto' => 'Lorem ipsum dolor sit amet consectetur adipiscing, elit morbi vehicula vivamus aptent dictumst tellus, diam cras pellentesque ornare tempus. Imperdiet ut litora auctor lectus justo porttitor, enim sapien dapibus libero tempor mauris, curae dignissim accumsan facilisi arcu. In sapien praesent eros ultrices nec posuere egestas scelerisque fermentum, laoreet augue fusce vitae suscipit ornare ad tincidunt aenean arcu, ultricies conubia nam dictum eget vel proin cursus.</p><p>Iaculis tellus mi massa lectus ligula aptent dis, venenatis nec ante fusce nam dignissim vehicula, habitasse erat mauris viverra natoque urna. Pellentesque penatibus facilisis aliquet tempor suscipit, in himenaeos orci nisl dis parturient, varius mattis eros euismod. A accumsan quisque duis hendrerit nullam sagittis etiam volutpat ultrices ultricies, rutrum iaculis laoreet urna eget lobortis lacinia congue inceptos dictum, interdum facilisis in odio ac erat malesuada mauris mus');	
	   
}

if($consulta == 'AcercaDe')
{	
	$array = array('Texto' => 'Lorem ipsum dolor sit amet consectetur adipiscing, elit morbi vehicula vivamus aptent dictumst tellus');	   
}

if($consulta == 'Politicas')
{		  
	$array = array('Texto' => 'Lorem ipsum dolor sit amet consectetur adipiscing, elit morbi vehicula vivamus aptent dictumst tellus, diam cras pellentesque ornare tempus. Imperdiet ut litora auctor lectus justo porttitor, enim sapien dapibus libero tempor mauris, curae dignissim accumsan facilisi arcu. In sapien praesent eros ultrices nec posuere egestas scelerisque fermentum, laoreet augue fusce vitae suscipit ornare ad tincidunt aenean arcu, ultricies conubia nam dictum eget vel proin cursus.</p><p>Iaculis tellus mi massa lectus ligula aptent dis, venenatis nec ante fusce nam dignissim vehicula, habitasse erat mauris viverra natoque urna. Pellentesque penatibus facilisis aliquet tempor suscipit, in himenaeos orci nisl dis parturient, varius mattis eros euismod. A accumsan quisque duis hendrerit nullam sagittis etiam volutpat ultrices ultricies, rutrum iaculis laoreet urna eget lobortis lacinia congue inceptos dictum.');		   
}

if($consulta == 'CompartirAPP')
{		  
	$array = array('URLUWP' => 'https://www.microsoft.com/es-co/store/appsvnext/windows-phone','URLiOS' => 'https://www.apple.com/co/ios/app-store/','URLAndroid' => 'https://play.google.com/store/apps/details?id=com.rutasdeautobuses.transmileniositp');		   
}


echo json_encode(new ArrayValue($array), JSON_PRETTY_PRINT);

?>