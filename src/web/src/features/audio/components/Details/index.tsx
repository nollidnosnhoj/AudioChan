import {
  Box,
  Flex,
  Heading,
  Stack,
  useColorModeValue,
  chakra,
} from "@chakra-ui/react";
import Router from "next/router";
import React, { useEffect } from "react";
import Link from "~/components/UI/Link";
import { AudioView } from "~/features/audio/api/types";
import { useUser } from "~/features/user/hooks";
import { relativeDate } from "~/utils/time";
import AudioPlayButton from "../Buttons/Play";
import AudioFavoriteButton from "../Buttons/Favorite";
import PictureController from "~/components/Picture";
import { useAddAudioPicture, useRemoveAudioPicture } from "../../api/hooks";
import AudioMiscMenu from "../ContextMenu";

interface AudioDetailProps {
  audio: AudioView;
}

const AudioDetails: React.FC<AudioDetailProps> = ({ audio }) => {
  const secondaryColor = useColorModeValue("black.300", "gray.300");
  const { user: currentUser } = useUser();

  const { mutateAsync: addPictureAsync, isLoading: isAddingPicture } =
    useAddAudioPicture(audio.id);

  const { mutateAsync: removePictureAsync, isLoading: isRemovingPicture } =
    useRemoveAudioPicture(audio.id);

  useEffect(() => {
    Router.prefetch(`/users/${audio.user.username}`);
  }, []);

  return (
    <Flex
      marginBottom={4}
      justifyContent="center"
      direction={{ base: "column", md: "row" }}
    >
      <Flex flex="1" marginRight={4} justify={{ base: "center", md: "normal" }}>
        <PictureController
          title={audio.title}
          src={audio.picture || ""}
          onChange={async (croppedData) => {
            await addPictureAsync(croppedData);
          }}
          onRemove={removePictureAsync}
          isMutating={isAddingPicture || isRemovingPicture}
          canEdit={currentUser?.id === audio.user.id}
        />
      </Flex>
      <Box flex="6">
        <chakra.div marginTop={{ base: 4, md: 0 }} marginBottom={4}>
          <Heading as="h1" fontSize={{ base: "3xl", md: "5xl" }}>
            {audio.title}
          </Heading>
          <chakra.div display="flex">
            <Link href={`/users/${audio.user.username}`} fontWeight="500">
              {audio.user.username}
            </Link>
            <chakra.span
              color={secondaryColor}
              _before={{ content: `"•"`, marginX: 2 }}
            >
              {relativeDate(audio.created)}
            </chakra.span>
          </chakra.div>
        </chakra.div>
        <Stack direction="row" alignItems="center">
          <AudioPlayButton audio={audio} size="lg" />
          <AudioFavoriteButton audioId={audio.id} size="lg" />
          <AudioMiscMenu audio={audio} size="lg" />
        </Stack>
      </Box>
    </Flex>
  );
};

export default AudioDetails;
