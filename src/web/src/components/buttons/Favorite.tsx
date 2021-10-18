import { IconButton } from "@chakra-ui/button";
import React from "react";
import { FaHeart } from "react-icons/fa";
import { ButtonProps } from "@chakra-ui/react";
import { ID } from "~/lib/types";
import { useUser } from "~/components/providers/UserProvider";
import { useFavoriteAudio } from "~/lib/hooks/api";

interface AudioFavoriteButtonProps extends ButtonProps {
  audioId: ID;
  isFavorite?: boolean;
}

export default function AudioFavoriteButton({
  audioId,
  isFavorite: initIsFavorite,
  ...buttonProps
}: AudioFavoriteButtonProps) {
  const { user } = useUser();
  const { isFavorite, favorite, isLoading } = useFavoriteAudio(
    audioId,
    initIsFavorite
  );

  if (!user) {
    return null;
  }

  return (
    <IconButton
      aria-label={isFavorite ? "Unfavorite" : "Favorite"}
      icon={<FaHeart />}
      colorScheme="primary"
      variant="ghost"
      opacity={isFavorite ? 1 : 0.5}
      isRound
      onClick={favorite}
      isLoading={isLoading}
      {...buttonProps}
    />
  );
}