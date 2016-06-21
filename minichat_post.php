<!DOCTYPE html>
<html>
	<head>
		<meta charset="utf-8">
		<title> Mini-chat </title>
	</head>

	<body>
		<?php
			// create a local cookie with the user's pseudonym (if is set)
			if (isset($_POST['pseudo']))
			{
				setcookie('pseudo', htmlspecialchars($_POST['pseudo']), time() + 365 * 24 * 3600, null, null, false, true);
			}

			// connect to the database
			try
			{
				$bdd = new PDO('mysql:host=localhost;dbname=test;charset=utf8', 'root', '');
			}
			catch(Exception $e)
			{
				die('Error: ' . $e->getMessage());
			}

			// store the message into the database
			if ( isset($_POST['pseudo']) AND isset($_POST['message']) )
			{
				$request = $bdd->prepare('INSERT INTO minichat (pseudo, message, date_creation) VALUES(:pseudo, :message, NOW()) ');
				$request->execute(array('pseudo' => htmlspecialchars($_POST['pseudo']), 'message' => htmlspecialchars($_POST['message'])));
			}

			// redirect to the minichat page
			header('Location: minichat.php');		
		?>
	</body>
</html>