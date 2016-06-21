<!DOCTYPE html>
<html>
	<head> 
		<meta charset="utf-8">
		<title> Mini-chat </title>

		<!-- javascript code to automatically scroll down to the bottom of div (containing the chat history) -->
		<script type="text/javascript">		
			function gotoBottom()
			{
   				var div = document.getElementById('history');
   				div.scrollTop = div.scrollHeight - div.clientHeight;
			}
   			window.onload = gotoBottom;
		</script>

	</head>

	<body>

		<!-- show the last 15 messages -->		
		<table frame="box" width="700" height="250">
		<tr><td>
		<div id="history" style="width:700px;height:250px;line-height:10px;overflow:auto;padding:1px;">
		
		<?php
			// connect to the database
			try
			{
				$bdd = new PDO('mysql:host=localhost;dbname=test;charset=utf8', 'root', '');
			}
			catch(Exception $e)
			{
				die('Error: ' . $e->getMessage());
			}

			// show the last messages using 2 nested queries : 
			// one to get the last 15 messages in reverse chronological order of the posting time (at the same time format the date into French style)
			// the second to reorder the messages in chronological order (by id or by 'date_creation_fr')

			$messages = $bdd->query('(SELECT id, pseudo, message, DATE_FORMAT(date_creation, \'%d/%m/%Y à %Hh%imin%ss\') AS date_creation_fr FROM minichat ORDER BY date_creation DESC LIMIT 0, 15) ORDER BY date_creation_fr');

			while ($message = $messages->fetch())
			{
				echo "<p> [" . htmlspecialchars($message['date_creation_fr']) . "] <strong> " . htmlspecialchars($message['pseudo']) ." </strong> : " . htmlspecialchars($message['message']) ." </p> ";
			}	

			$messages->closeCursor();
		?>
		</div>
		</td></tr>
		</table>


		<!-- the form to 'refresh' the chat (in case other users posted messages after us) -->
		<form action="minichat.php" method="post">
			<p>
			<input type="submit" value="Refresh chat">
			</p>
		</form>

		<br />

		<!-- the form to send new messages -->
		<form action="minichat_post.php" method="post">
			<p>
			<table>
			<tr>
			<td><label for="pseudo"> Pseudo </label> : </td>
			<td>

			<!-- automatically fill in the pseudonym when the cookie is set -->
			<input type="text" name="pseudo" id="pseudo" 
				<?php if ( isset($_COOKIE['pseudo'])) { echo 'value="' . htmlspecialchars($_COOKIE['pseudo']). '"'; } ?> /> 
			</td>
			</tr>

			<tr>
			<td> <label for="message"> Message </label> : </td>
			<td> <input type="text" name="message" id="message" /> </td>
			</tr>
			</table>

			<br />
			<input type="submit" value="Envoyer message">
			</p>
		</form>

	</body>
</html>