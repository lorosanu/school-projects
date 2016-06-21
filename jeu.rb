
#================================
# definitions des classes
#================================

class Personne
  attr_accessor :nom, :points_de_vie, :en_vie

  def initialize(nom)
    @nom = nom
    @points_de_vie = 100
    @en_vie = true
  end

  def info
    @en_vie ? "#{@nom} (#{@points_de_vie}/100 pv)" : "#{nom} (vaincu)"
  end

  def attaque(personne)
    puts "#{@nom} attaque #{personne.nom}"
    personne.subit_attaque(degats()) 		# la personne attaquée subit des dégats 
    										# le nombre des points perdus deppend de l'identité de l'attaquant
											# (appel de la methode propre à la classe fille)
  end

  def subit_attaque(degats_recus)
    if @points_de_vie <= 0 
      puts "Dommage. #{@nom} était deja mort ..."
    else
      @points_de_vie -= degats_recus
      puts "#{@nom} subit #{degats_recus}hp de dégats"

      if @points_de_vie <= 0
        @en_vie = false
        puts "#{@nom} a été vaincu"
      end
    end  
  end
end

class Joueur < Personne
  attr_accessor :degats_bonus

  def initialize(nom)
    @degats_bonus = 0						# par défaut, le joueur n'a pas de dégats bonus
    super(nom) 								# appelle le "initialize" de la classe mère (Personne)
  end

  def degats
    puts "#{@nom} profite de #{@degats_bonus} points de dégats bonus"
    lance_des = 3 * ( 1 + rand(20) )        # choix personnel : simuler le lancement de trois dés de 20
    lance_des + @degats_bonus    
  end

  def soin
    lance_des = 2 * (1 + rand(20))    		# choix personnel : simuler le lancement de deux dés de 20
    @points_de_vie += lance_des  	

    puts "#{@nom} regagne #{lance_des} points de vie"

    if @points_de_vie > 100
      @points_de_vie = 100
    end
  end

  def ameliorer_degats
    lance_des = 1 + rand(20)    			# choix personnel : simuler le lancement d'un seul dé de 20
    puts "#{@nom} gagne en puissance : plus #{lance_des} points à ses dégats"
    @degats_bonus += lance_des
  end
end

class Ennemi < Personne
  def degats
    1 + rand(12)							# choix personnel : simuler le lancement d'un seul dé de 12
  end
end

class Jeu
  def self.actions_possibles(monde)
    puts "ACTIONS POSSIBLES :"

    puts "0 - Se soigner"
    puts "1 - Améliorer son attaque"

    # action > 2 : attaquer les enemis
    i = 2
    monde.ennemis.each do |ennemi|
      puts "#{i} - Attaquer #{ennemi.info}"
      i = i + 1
    end
    puts "99 - Quitter"
  end

  def self.est_fini(joueur, monde)
    jeu_fini = true

    monde.ennemis.each do |ennemi|
      if ennemi.en_vie
        jeu_fini = false
      end
    end

    if jeu_fini
      return true
    else
      if ! joueur.en_vie
	return true
      else
        return false
      end
    end
  end
end

class Monde
  attr_accessor :ennemis

  def ennemis_en_vie
    liste_ennemis = []

    @ennemis.each do |ennemi|
      if ennemi.en_vie
        liste_ennemis << ennemi
      end
    end

    liste_ennemis
  end
end



#==============================
# Initialisations
#==============================


# Initialisation du monde
monde = Monde.new

# Ajout des ennemis
monde.ennemis = [
  Ennemi.new("Balrog"),
  Ennemi.new("Goblin"),
  Ennemi.new("Squelette")
]


# Initialisation du joueur
joueur = Joueur.new("Jean-Michel Paladin")


#==============================
# Jeu principal
#==============================


puts "\n\nAinsi débutent les aventures de #{joueur.nom}\n\n"


100.times do |tour|
  puts "\n------------------ Tour numéro #{tour} ------------------"

  Jeu.actions_possibles(monde)				# affiche les différentes actions possibles

  puts "\nQUELLE ACTION FAIRE ?"
  STDOUT.flush

  choix = gets.chomp.to_i					# la variable "choix" stocke ce que l'utilisateur renseigne

  case choix
  when 0									# soigner ses blessures
    joueur.soin
  when 1									# ameliorer ses degats
    joueur.ameliorer_degats
  when 2..(monde.ennemis.size + 1)			# attaquer un enemi (options 2, ...)
    ennemi_a_attaquer = monde.ennemis[choix - 2]
    joueur.attaque(ennemi_a_attaquer)
  when 99						# quitter la partie
    puts "Vous quittez la partie !"
    break
  else
    puts "Mauvaise nouvelle ... votre choix est invalide..."
  end

  puts "\nLES ENNEMIS RIPOSTENT !"
  monde.ennemis_en_vie.each do |ennemi|		# pour les enemies en vie
    ennemi.attaque(joueur)					# le héro subit une attaque
  end

  puts "\nEtat du héro: #{joueur.info}\n"

  break if Jeu.est_fini(joueur, monde)		# si le jeu est fini, on interompt la boucle
end



#==============================
# fin de la partie
#==============================

puts "\nGame Over!\n\n"

puts "Etat du jeu en fin de partie : "
puts "================================================================"
puts "Votre personnage : #{joueur.info}"
puts "Vos ennemis :      #{monde.ennemis_en_vie.size} ennemi(s) en vie "

monde.ennemis_en_vie.each do |ennemi|
  puts "\t\t\t" + ennemi.info
end

if joueur.en_vie
  if Jeu.est_fini(joueur, monde) 
    puts "\nVous avez gagné !"
  end
else
  puts "\nVous avez perdu !"
end

