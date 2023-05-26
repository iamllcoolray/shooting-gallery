package com.game.shooting.gallery;

import com.badlogic.gdx.ApplicationAdapter;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Input;
import com.badlogic.gdx.graphics.Cursor;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.BitmapFont;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.math.Vector2;
import com.badlogic.gdx.utils.ScreenUtils;

import java.util.Random;
import java.util.Vector;

public class ShootingGalleryGame extends ApplicationAdapter {
	SpriteBatch batch;
	BitmapFont font;
	Texture sky, target, crosshairs;
	Vector2 mousePosition, targetPosition;
	Random random;
	int score = 0;
	
	@Override
	public void create () {
		batch = new SpriteBatch();
		font = new BitmapFont();

		sky = new Texture("sky.png");
		target = new Texture("target.png");
		crosshairs = new Texture("crosshairs.png");

		random = new Random();
		targetPosition = new Vector2(random.nextFloat(0, Gdx.graphics.getWidth()), random.nextFloat(0, Gdx.graphics.getHeight()));

		Gdx.graphics.setResizable(false);
		Gdx.input.setCursorCatched(true);
	}

	@Override
	public void render () {
		ScreenUtils.clear(0, 0, 0, 1);
		mousePosition = new Vector2(Gdx.input.getX() - 20, Gdx.graphics.getHeight() - Gdx.input.getY() - 20);
		if(Gdx.input.isButtonPressed(Input.Buttons.LEFT)){
			if (distance(mousePosition, targetPosition) < 50) {
				targetPosition = new Vector2(random.nextFloat(0, Gdx.graphics.getWidth()), random.nextFloat(0, Gdx.graphics.getHeight()));
				score++;
			}
		}
		batch.begin();
		batch.draw(sky, 0, 0);
		font.draw(batch, "Score: " + score, 20, Gdx.graphics.getHeight() - 20);
		batch.draw(target, targetPosition.x - (float) target.getWidth() /2, targetPosition.y - (float) target.getHeight() /2);
		batch.draw(crosshairs, mousePosition.x, mousePosition.y);
		batch.end();
	}
	
	@Override
	public void dispose () {
		batch.dispose();
		font.dispose();
		sky.dispose();
		target.dispose();
		crosshairs.dispose();
	}

	public double distance(Vector2 mouse, Vector2 sprite){
		return (Math.sqrt(Math.pow(sprite.x - mouse.x, 2) + Math.pow(sprite.y - mouse.y, 2)));
	}
}
