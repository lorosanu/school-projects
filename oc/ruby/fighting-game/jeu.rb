#!/usr/bin/ruby

#================================
# definitions des classes
#================================

class Personne
  attr_reader :nom, :points_de_vie, :en_vie

  def initialize(nom)
    @nom = nom
    @points_de_vie = 100
    @en_vie = true
  end

  def info
    @en_vie ? "#{@nom} (#{@points_de_vie}/100 pv)" : "#{nom} (vaincu)"
  end

  def attaque(personne)
    puts("#{@nom} attaque #{personne.nom}")

    # la personne attaquée subit des dégats
    # le nombre des points perdus deppend de l'identité de l'attaquant
    # (appel de la methode propre à la classe fille)
    personne.subit_attaque(degats())
  end

  def subit_attaque(degats_recus)
    if @points_de_vie <= 0
      puts("   Dommage, #{@nom} était deja mort ...")
    else
      @points_de_vie -= degats_recus
      puts("-- #{@nom} subit #{degats_recus}hp de dégats")

      if @points_de_vie <= 0
        @en_vie = false
        puts("#{@nom} a été vaincu")
      end
    end
  end
end

class Joueur < Personne
  attr_reader :degats_bonus

  def initialize(nom)
    # appelle le 'initialize' de la classe mère (Personne)
    super(nom)

    # par défaut, le joueur n'a pas de dégats bonus
    @degats_bonus = 0
  end

  def degats
    puts("#{@nom} profite de #{@degats_bonus} points de dégats bonus")

    # choix personnel : simuler le lancement de trois dés de 20
    lance_des = 3.times.lazy.reduce(0){|s| s + rand(1..20) }
    lance_des + @degats_bonus
  end

  def soin
    # choix personnel : simuler le lancement de deux dés de 20
    lance_des = 2.times.lazy.reduce(0){|s| s + rand(1..20) }

    @points_de_vie += lance_des
    puts("++ #{@nom} regagne #{lance_des} points de vie")

    @points_de_vie = 100 if @points_de_vie > 100
  end

  def ameliorer_degats
    # choix personnel : simuler le lancement d'un seul dé de 20
    lance_des = rand(1..20)
    puts("++ #{@nom} gagne en puissance : plus #{lance_des} points à ses dégats")
    @degats_bonus += lance_des
  end
end

class Ennemi < Personne
  def degats
    # choix personnel : simuler le lancement d'un seul dé de 12
    1 + rand(12)
  end
end

class Jeu
  def self.actions_possibles(monde)
    puts "ACTIONS POSSIBLES :"

    puts "0 - Se soigner"
    puts "1 - Améliorer son attaque"

    # action > 2 : attaquer les enemis
    monde.ennemis.each_with_index do |ennemi, index|
      puts "#{index + 2} - Attaquer #{ennemi.info}"
    end

    puts "99 - Quitter"
  end

  def self.est_fini(joueur, monde)
    return true if monde.ennemis.none?{|ennemi| ennemi.en_vie }
    return true unless joueur.en_vie
	  false
  end
end

class Monde
  attr_accessor :ennemis

  def initialize(liste_ennemis=[])
    @ennemis = []
    liste_ennemis.each{|ennemi| ajout_ennemi(ennemi) }
  end

  def ajout_ennemi(ennemi)
    @ennemis << ennemi
  end

  def ennemis_en_vie
    @ennemis.select{|ennemi| ennemi.en_vie }
  end
end


#==============================
# Initialisations
#==============================

# Initialisation du monde
monde = Monde.new()

# Ajout des ennemis
monde.ajout_ennemi(Ennemi.new('Balrog'))
monde.ajout_ennemi(Ennemi.new('Goblin'))
monde.ajout_ennemi(Ennemi.new('Squelette'))

# Initialisation du joueur
joueur = Joueur.new('Jean-Michel Paladin')


#==============================
# Jeu principal
#==============================

puts("\nAinsi débutent les aventures de #{joueur.nom}\n\n")

100.times do |tour|
  puts(" Tour numéro #{tour} ".center(70, '-'), "\n")

  # affiche les différentes actions possibles
  Jeu.actions_possibles(monde)

  puts("\nQUELLE ACTION FAIRE ?")
  STDOUT.flush

  # la variable "choix" stocke ce que l'utilisateur renseigne
  choix = gets.chomp.to_i

  case choix
  when 0
    # soigner ses blessures
    joueur.soin
  when 1
    # ameliorer ses degats
    joueur.ameliorer_degats
  when 2..(monde.ennemis.size + 1)
    # attaquer un enemi (options 2, ...)
    ennemi_a_attaquer = monde.ennemis[choix - 2]
    joueur.attaque(ennemi_a_attaquer)
  when 99
    # quitter la partie
    puts("Vous quittez la partie !")
    break
  else
    puts("Mauvaise nouvelle ... votre choix est invalide...")
  end

  if monde.ennemis.any?{|ennemi| ennemi.en_vie }
    # le héro subit une attaque de chaque enemi encore en vie
    puts("\nLES ENNEMIS RIPOSTENT !")
    monde.ennemis_en_vie.each{|ennemi| ennemi.attaque(joueur) }
  end

  puts("\nEtat du héro: #{joueur.info}\n\n")

  # si le jeu est fini, on interompt la boucle
  break if Jeu.est_fini(joueur, monde)
end


#==============================
# fin de la partie
#==============================

puts("\nGame Over!\n\n")

puts('Etat du jeu en fin de partie : ')
puts('-' * 70)
puts("Votre personnage : #{joueur.info}")
puts("Vos ennemis :      #{monde.ennemis_en_vie.size} ennemi(s) en vie ")
monde.ennemis_en_vie.each{|ennemi| puts(ennemi.info.to_s.rjust(40)) }

if joueur.en_vie
  puts("\nVous avez gagné !") if Jeu.est_fini(joueur, monde)
else
  puts("\nVous avez perdu !")
end
